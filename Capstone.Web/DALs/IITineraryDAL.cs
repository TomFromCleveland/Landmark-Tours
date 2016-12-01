using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DALs
{
    interface IITineraryDAL
    {
        bool CreateNewItinerary(ItineraryModel itinerary);
        bool DeleteItinerary(ItineraryModel itinerary);
    }
}
