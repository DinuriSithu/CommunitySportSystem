using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommunitySportSystem.Models;

namespace CommunitySportSystem.Controllers
{
    public class HomeController : Controller
    {
        private CommunitySportsDbContext db = new CommunitySportsDbContext();

        public ActionResult Index()
        {
            ViewBag.Sports = db.Sports.ToList();
            ViewBag.FacilityCount = db.Facilities.Count();

            return View();
        }
    }
}