using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunitySportSystem.Models;
using CommunitySportSystem.ViewModels;

namespace CommunitySportSystem.Controllers
{
    public class BookingController : Controller
    {
        private CommunitySportsDbContext db = new CommunitySportsDbContext();

        public ActionResult Create(int facilityID)
        {
            if (Session["MemberID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var facility = db.Facilities
                .FirstOrDefault(f => f.Facility_ID == facilityID);

            if (facility == null)
            {
                return HttpNotFound();
            }

            var model = new BookingViewModel
            {
                Facility_ID = facility.Facility_ID,
                FacilityName = facility.FacilityName,
                BookingDate = DateTime.Today
            };

            ViewBag.ExistingBookings = db.Bookings
                .Where(b => b.Facility_ID == facilityID &&
                            b.BookingStatus != "Cancelled" &&
                            b.BookingDate >= DateTime.Today)
                .OrderBy(b => b.BookingDate).ThenBy(b => b.StartTime)
                .ToList();

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookingViewModel model)
        {

            if (Session["MemberID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                if (model.StartTime == TimeSpan.Zero || model.EndTime == TimeSpan.Zero)
                {
                    ModelState.AddModelError(
                        "",
                        "Please select both a start time and an end time."
                    );
                }
                else if (model.EndTime <= model.StartTime)
                {
                    ModelState.AddModelError(
                        "EndTime",
                        "End time must be after start time."
                    );
                }
                else
                {
                    var facility = db.Facilities
                        .FirstOrDefault(f => f.Facility_ID == model.Facility_ID);

                    if (facility == null)
                    {
                        return HttpNotFound();
                    }

                    bool alreadyBooked = db.Bookings.Any(b =>
                        b.Facility_ID == model.Facility_ID &&
                        b.BookingDate == model.BookingDate &&
                        b.BookingStatus != "Cancelled" &&
                        model.StartTime < b.EndTime &&
                        model.EndTime > b.StartTime
                    );

                    if (alreadyBooked)
                    {
                        ModelState.AddModelError(
                            "",
                            "This facility is already booked for the selected date and time."
                        );
                    }
                    else
                    {
                        var booking = new Booking
                        {
                            Member_ID = (int)Session["MemberID"],
                            Facility_ID = model.Facility_ID,
                            BookingDate = model.BookingDate,
                            StartTime = model.StartTime,
                            EndTime = model.EndTime,
                            BookingStatus = "Confirmed",
                            CreatedDate = DateTime.Now
                        };

                        db.Bookings.Add(booking);
                        db.SaveChanges();

                        return RedirectToAction("MyBookings");
                    }
                }
            }

            var selectedFacility = db.Facilities
                .FirstOrDefault(f => f.Facility_ID == model.Facility_ID);

            if (selectedFacility != null)
            {
                model.FacilityName = selectedFacility.FacilityName;
            }

            ViewBag.ExistingBookings = db.Bookings
                .Where(b => b.Facility_ID == model.Facility_ID &&
                            b.BookingStatus != "Cancelled" &&
                            b.BookingDate >= DateTime.Today)
                .OrderBy(b => b.BookingDate).ThenBy(b => b.StartTime)
                .ToList();

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            if (Session["MemberID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int memberID = (int)Session["MemberID"];

            var booking = db.Bookings
                .FirstOrDefault(b => b.Booking_ID == id && b.Member_ID == memberID);

            if (booking == null)
            {
                return HttpNotFound();
            }

            if (booking.BookingStatus == "Cancelled" || booking.BookingDate < DateTime.Today)
            {
                return RedirectToAction("MyBookings");
            }

            var model = new BookingViewModel
            {
                Booking_ID = booking.Booking_ID,
                Facility_ID = booking.Facility_ID,
                FacilityName = booking.Facility.FacilityName,
                BookingDate = booking.BookingDate,
                StartTime = booking.StartTime,
                EndTime = booking.EndTime
            };

            ViewBag.ExistingBookings = db.Bookings
                .Where(b => b.Facility_ID == booking.Facility_ID &&
                            b.Booking_ID != booking.Booking_ID &&
                            b.BookingStatus != "Cancelled" &&
                            b.BookingDate >= DateTime.Today)
                .OrderBy(b => b.BookingDate).ThenBy(b => b.StartTime)
                .ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookingViewModel model)
        {
            if (Session["MemberID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int memberID = (int)Session["MemberID"];

            if (ModelState.IsValid)
            {
                if (model.StartTime == TimeSpan.Zero || model.EndTime == TimeSpan.Zero)
                {
                    ModelState.AddModelError("", "Please select both a start time and an end time.");
                }
                else if (model.EndTime <= model.StartTime)
                {
                    ModelState.AddModelError("EndTime", "End time must be after start time.");
                }
                else
                {
                    var booking = db.Bookings
                        .FirstOrDefault(b => b.Booking_ID == model.Booking_ID && b.Member_ID == memberID);

                    if (booking == null)
                    {
                        return HttpNotFound();
                    }

                    bool alreadyBooked = db.Bookings.Any(b =>
                        b.Facility_ID == booking.Facility_ID &&
                        b.Booking_ID != booking.Booking_ID &&
                        b.BookingDate == model.BookingDate &&
                        b.BookingStatus != "Cancelled" &&
                        model.StartTime < b.EndTime && model.EndTime > b.StartTime
                    );

                    if (alreadyBooked)
                    {
                        ModelState.AddModelError(
                            "",
                            "This facility is already booked for the selected date and time."
                        );
                    }
                    else
                    {
                        booking.BookingDate = model.BookingDate;
                        booking.StartTime = model.StartTime;
                        booking.EndTime = model.EndTime;
                        db.SaveChanges();

                        return RedirectToAction("MyBookings");
                    }
                }
            }

            var facility = db.Facilities.FirstOrDefault(f => f.Facility_ID == model.Facility_ID);
            if (facility != null)
            {
                model.FacilityName = facility.FacilityName;
            }

            ViewBag.ExistingBookings = db.Bookings
                .Where(b => b.Facility_ID == model.Facility_ID &&
                            b.Booking_ID != model.Booking_ID &&
                            b.BookingStatus != "Cancelled" &&
                            b.BookingDate >= DateTime.Today)
                .OrderBy(b => b.BookingDate).ThenBy(b => b.StartTime)
                .ToList();

            return View(model);
        }

        public ActionResult MyBookings()
        {
            if (Session["MemberID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int memberID = (int)Session["MemberID"];

            var bookings = db.Bookings
                .Where(b => b.Member_ID == memberID)
                .OrderByDescending(b => b.BookingDate)
                .ThenBy(b => b.StartTime)
                .ToList();

            return View(bookings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int id)
        {
            if (Session["MemberID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int memberID = (int)Session["MemberID"];

            var booking = db.Bookings
                .FirstOrDefault(b => b.Booking_ID == id && b.Member_ID == memberID);

            if (booking == null)
            {
                return HttpNotFound();
            }

            if (booking.BookingStatus != "Cancelled")
            {
                booking.BookingStatus = "Cancelled";
                db.SaveChanges();
            }

            return RedirectToAction("MyBookings");
        }
    }
}