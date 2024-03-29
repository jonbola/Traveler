using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Traveler.Models;

namespace Traveler.Design_Pattern.Factory_Pattern
{
    public class DefaultTourSearchFactory:ITourSearchFactory
    {
        public List<Tour> SearchTours(TravelBookingEntities db, string departureLocation, string destination, DateTime? departureDay, (int, int) priceInterval)
        {
            return db.Tours.Where(t=>t.DepartureLocation.DepartureName.Contains(departureLocation)&& 
            t.Destination.DestinationName.Contains(destination) && 
            t.Price >= priceInterval.Item1 * 1000000 &&
            t.Price <= priceInterval.Item2 * 1000000).ToList();

        }
    }
    public class DepartureDayTourSearchFactory : ITourSearchFactory
    {
        public List<Tour> SearchTours(TravelBookingEntities db, string departureLocation, string destination, DateTime? departureDay, (int, int) priceInterval)
        {
            return db.Tours
                     .Where(t => t.DepartureLocation.DepartureName.Contains(departureLocation) &&
                                 t.Destination.DestinationName.Contains(destination) &&
                                 (departureDay == null || t.DepartureDay == departureDay) &&
                                 t.Price >= priceInterval.Item1 * 1000000 &&
                                 t.Price <= priceInterval.Item2 * 1000000)
                     .ToList();
        }
    }
}