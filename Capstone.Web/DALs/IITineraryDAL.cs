using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Web.DALs
{
    public interface IItineraryDAL
    {
        ItineraryModel CreateNewItinerary(ItineraryModel itinerary);
        bool DeleteItinerary(ItineraryModel itinerary);
        List<ItineraryModel> GetAllItineraries(int userId);
        ItineraryModel GetItineraryDetail(int itineraryID);
        ItineraryModel GetItineraryByID(int itineraryID);
        bool AddLandmarkToItinerary(int landmarkID, int itineraryID);
        bool DeleteLandmarkFromItinerary(int landmarkID, int itineraryID);
    }
}
