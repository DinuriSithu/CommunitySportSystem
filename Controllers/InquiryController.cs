using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunitySportSystem.Models;
using CommunitySportSystem.ViewModels;

namespace CommunitySportSystem.Controllers
{
    public class InquiryController : Controller
    {
        private CommunitySportsDbContext db = new CommunitySportsDbContext();

        public ActionResult Create()
        {
            var model = new InquiryViewModel();

            if (Session["MemberID"] != null)
            {
                int memberID = (int)Session["MemberID"];

                var member = db.Members
                    .FirstOrDefault(m => m.Member_ID == memberID);

                if (member != null)
                {
                    model.Name = member.FirstName + " " + member.LastName;
                    model.Email = member.Email;
                }
                ViewBag.MyInquiries = db.Inquiries
                    .Where(i => i.Member_ID == memberID)
                    .OrderByDescending(i => i.Inq_Date)
                    .ToList();
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InquiryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var inquiry = new Inquiry
                {
                    Name = model.Name,
                    Email = model.Email,
                    Subject = model.Subject,
                    Message = model.Message,
                    Inq_Date = DateTime.Now,
                    Inq_Status = "Pending"
                };

                if (Session["MemberID"] != null)
                {
                    inquiry.Member_ID = (int)Session["MemberID"];
                }

                db.Inquiries.Add(inquiry);
                db.SaveChanges();

                return RedirectToAction("Success");
            }

            if (Session["MemberID"] != null)
            {
                int memberID = (int)Session["MemberID"];

                ViewBag.MyInquiries = db.Inquiries
                    .Where(i => i.Member_ID == memberID)
                    .OrderByDescending(i => i.Inq_Date)
                    .ToList();
            }

            return View(model);
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}