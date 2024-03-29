using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traveler.Models;

namespace Traveler.Design_Pattern.Factory_Pattern
{
    internal interface ITourSearchFactory
    {
        List<Tour> SearchTours(TravelBookingEntities db, string departure, string destination, DateTime? departureDay, (int, int) priceInterval);
    }
}
