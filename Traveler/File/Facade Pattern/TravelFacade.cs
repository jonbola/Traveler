using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Traveler.Models;
using Traveler.Design_Pattern;
using Traveler.Facades;

namespace Traveler.Design_Pattern.Facade_Pattern
{
    public class TravelFacade : ITravelFacade
    {
        private TravelBookingEntities _db = new TravelBookingEntities();

        public List<TourGuide> GetTourGuides()
        {
            return _db.TourGuides.ToList();
        }

        public List<Tour> GetTours()
        {
            List<Tour> toursList = _db.Tours.ToList();
            toursList.Reverse();
            return toursList;
        }

        public List<Destination> GetDestinations()
        {
            return _db.Destinations.ToList();
        }

        public List<DepartureLocation> GetDepartureLocations()
        {
            return _db.DepartureLocations.ToList();
        }
    }
}