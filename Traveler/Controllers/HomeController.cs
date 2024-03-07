using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Traveler.Models;

namespace Traveler.Controllers
{
    public class HomeController : Controller
    {
        TravelBookingEntities db = new TravelBookingEntities();
        public ActionResult Index()
        {
            ViewBag.TourGuidesList = db.TourGuides.ToList();
            List<Tour> toursList = db.Tours.ToList();
            toursList.Reverse();
            ViewBag.ToursList = toursList;
            ViewBag.DestinationLst = db.Destinations.ToList();
            ViewBag.DepartureLocationLst = db.DepartureLocations.ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}