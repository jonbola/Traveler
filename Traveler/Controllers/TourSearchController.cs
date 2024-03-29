using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Traveler.Design_Pattern.Factory_Pattern;
using Traveler.Models;

namespace Traveler.Controllers
{
    public class TourSearchController : Controller
    {
        TravelBookingEntities db = new TravelBookingEntities();

        [HttpGet]
        // GET: TourSearch
        public ActionResult FillResult()
        {
            return View();
        }

        [HttpPost]
        // GET: TourSearch
        public ActionResult FillResult(FormCollection form)
        {
            //    string departureLocation = form["DepartureLocation"].ToString().Trim();
            //    string destination = form["Destination"].ToString().Trim();
            //    string departureDayString = form["DepartureDay"];
            //    int durationType = int.Parse(form["PriceRange"]);
            //    DateTime departureDay;
            //    var inteval = (0,0);

            //    switch (durationType)
            //    {
            //        case 1: { inteval = (1, 3); break; }
            //        case 2: { inteval = (4, 7); break; }
            //        case 3: { inteval = (8, 100); break; }
            //        default: { inteval = (0,100); break; }
            //    }

            //    //if(departureLocation == null)
            //    //{

            //    //}

            //    if (DateTime.TryParse(departureDayString, out departureDay))
            //    {
            //        departureDay = DateTime.Parse(departureDayString);
            //        var lstTour = db.Tours
            //            .Where(t => t.DepartureLocation.DepartureName.Contains(departureLocation) &&
            //            t.Destination.DestinationName.Contains(destination) &&
            //            (departureDay == null || t.DepartureDay == departureDay) &&                    
            //            t.Price >= inteval.Item1*1000000 && t.Price <= inteval.Item2*1000000).ToList();

            //        ViewBag.ToursList = lstTour;

            //    }
            //    else
            //    {
            //        ViewBag.ToursList = db.Tours.Where(t => t.DepartureLocation.DepartureName.Contains(departureLocation) && t.Destination.DestinationName.Contains(destination) && t.Price >= inteval.Item1 * 1000000 && t.Price <= inteval.Item2 * 1000000).ToList();

            //    }





            //    return View();
            //}
            string departureLocation = form["DepartureLocation"].ToString().Trim();
            string destination = form["Destination"].ToString().Trim();
            string departureDayString = form["DepartureDay"];
            int durationType = int.Parse(form["PriceRange"]);
            DateTime departureDay;
            var interval = (0, 0);

            switch (durationType)
            {
                case 1: { interval = (1, 3); break; }
                case 2: { interval = (4, 7); break; }
                case 3: { interval = (8, 100); break; }
                default: { interval = (0, 100); break; }
            }

            ITourSearchFactory searchFactory;
            if (DateTime.TryParse(departureDayString, out departureDay))
            {
                searchFactory = new DepartureDayTourSearchFactory();
            }
            else
            {
                searchFactory = new DefaultTourSearchFactory();
            }

            var lstTour = searchFactory.SearchTours(db, departureLocation, destination, departureDay, interval);
            ViewBag.ToursList = lstTour;

            return View();
        }
    }
}