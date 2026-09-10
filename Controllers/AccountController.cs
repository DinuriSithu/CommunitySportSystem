using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunitySportSystem.Models;
using CommunitySportSystem.ViewModels;

namespace CommunitySportSystem.Controllers
{
    public class AccountController : Controller
    {
        private CommunitySportsDbContext db = new CommunitySportsDbContext();

        public ActionResult Register()
        {
            var model = new RegisterViewModel
            {
                Sports = db.Sports
                    .Select(s => new SelectListItem
                    {
                        Value = s.Sport_ID.ToString(),
                        Text = s.SportName
                    })
                    .ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (db.Members.Any(m => m.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "This email address is already registered.");
                }
                else
                {
                    var member = new Member
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = model.Password,
                        Phone = model.Phone,
                        Address = model.Address,
                        RegistrationDate = DateTime.Now
                    };

                    db.Members.Add(member);
                    db.SaveChanges();

                    if (model.SelectedSportIDs != null)
                    {
                        foreach (var sportID in model.SelectedSportIDs)
                        {
                            var memberSport = new Member_Sport
                            {
                                Member_ID = member.Member_ID,
                                Sport_ID = sportID
                            };

                            db.MemberSports.Add(memberSport);
                        }
                        db.SaveChanges();
                    }
                    return RedirectToAction("Login");
                }
            }

            model.Sports = db.Sports
                .Select(s => new SelectListItem
                {
                    Value = s.Sport_ID.ToString(),
                    Text = s.SportName
                })
                .ToList();

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var member = db.Members
                    .FirstOrDefault(m => m.Email == model.Email && m.Password == model.Password);

                if (member != null)
                {
                    Session["MemberID"] = member.Member_ID;
                    Session["MemberName"] = member.FirstName + " " + member.LastName;

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}