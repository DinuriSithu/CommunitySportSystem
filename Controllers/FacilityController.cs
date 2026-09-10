using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunitySportSystem.Models;

namespace CommunitySportSystem.Controllers
{
    public class FacilityController : Controller
    {
        private CommunitySportsDbContext db = new CommunitySportsDbContext();


        public ActionResult Facilities(string search, int? facilityTypeID, DateTime? bookingDate, TimeSpan? startTime, TimeSpan? endTime)
        {
            var facilities = db.Facilities.AsQueryable();

            bool isGuest = Session["MemberID"] == null;
            if (isGuest)
            {
                bookingDate = null;
                startTime = null;
                endTime = null;
            }

            if (!string.IsNullOrEmpty(search))
            {
                facilities = facilities.Where(f =>
                    f.FacilityName.Contains(search) ||
                    f.Location.Contains(search));
            }

            if (facilityTypeID.HasValue)
            {
                facilities = facilities.Where(f =>
                    f.FacilityTypeID == facilityTypeID.Value);
            }

            if (bookingDate.HasValue)
            {
                var searchDate = bookingDate.Value.Date;

                var conflicting = db.Bookings.Where(b =>
                    b.BookingDate == searchDate &&
                    b.BookingStatus != "Cancelled");

                if (startTime.HasValue && endTime.HasValue)
                {
                    conflicting = conflicting.Where(b =>
                        startTime.Value < b.EndTime && endTime.Value > b.StartTime);
                }

                var bookedFacilityIDs = conflicting
                    .Select(b => b.Facility_ID)
                    .Distinct()
                    .ToList();

                facilities = facilities.Where(f => !bookedFacilityIDs.Contains(f.Facility_ID));
            }

            var facilityList = facilities.ToList();

            var facilityIDs = facilityList.Select(f => f.Facility_ID).ToList();

            var ratingStats = db.Reviews
                .Where(r => facilityIDs.Contains(r.Facility_ID))
                .GroupBy(r => r.Facility_ID)
                .Select(g => new
                {
                    FacilityID = g.Key,
                    Average = g.Average(r => r.Rating),
                    Count = g.Count()
                })
                .ToList();

            ViewBag.AverageRatings = ratingStats.ToDictionary(r => r.FacilityID, r => r.Average);
            ViewBag.ReviewCounts = ratingStats.ToDictionary(r => r.FacilityID, r => r.Count);

            ViewBag.Search = search;
            ViewBag.FacilityTypeID = facilityTypeID;
            ViewBag.BookingDate = bookingDate;
            ViewBag.StartTime = startTime;
            ViewBag.EndTime = endTime;

            ViewBag.FacilityTypes = db.FacilityTypes.ToList();

            return View(facilityList);
        }
        public ActionResult Details(int id)
        {
            var facility = db.Facilities
                .FirstOrDefault(f => f.Facility_ID == id);

            if (facility == null)
            {
                return HttpNotFound();
            }

            var facilityReviews = db.Reviews.Where(r => r.Facility_ID == id);

            ViewBag.AverageRating = facilityReviews.Select(r => (double?)r.Rating).Average() ?? 0;
            ViewBag.ReviewCount = facilityReviews.Count();

            return View(facility);
        }
    }
}