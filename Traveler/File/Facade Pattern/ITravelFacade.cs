using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Models;

namespace Traveler.Facades
{
    internal interface ITravelFacade
    {
        List<TourGuide> GetTourGuides();
        List<Tour> GetTours();
        List<Destination> GetDestinations();
        List<DepartureLocation> GetDepartureLocations();
    }
}
