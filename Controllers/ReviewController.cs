using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunitySportSystem.Models;
using CommunitySportSystem.ViewModels;

namespace CommunitySportSystem.Controllers
{
    public class ReviewController : Controller
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

            var model = new ReviewViewModel
            {
                FacilityID = facility.Facility_ID,
                FacilityName = facility.FacilityName
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReviewViewModel model)
        {
            if (Session["MemberID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                var facility = db.Facilities
                    .FirstOrDefault(f => f.Facility_ID == model.FacilityID);

                if (facility == null)
                {
                    return HttpNotFound();
                }

                var review = new Review
                {
                    Member_ID = (int)Session["MemberID"],
                    Facility_ID = model.FacilityID,
                    Rating = model.Rating,
                    Comment = model.Comment,
                    ReviewDate = DateTime.Now
                };

                db.Reviews.Add(review);
                db.SaveChanges();

                return RedirectToAction("Reviews");
            }

            var selectedFacility = db.Facilities
                .FirstOrDefault(f => f.Facility_ID == model.FacilityID);

            if (selectedFacility != null)
            {
                model.FacilityName = selectedFacility.FacilityName;
            }

            return View(model);
        }

        public ActionResult Reviews()
        {
            var reviews = db.Reviews
                .OrderByDescending(r => r.ReviewDate)
                .ToList();

            return View(reviews);
        }
    }
}