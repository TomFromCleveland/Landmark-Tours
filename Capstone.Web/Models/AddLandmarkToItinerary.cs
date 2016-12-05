using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class AddLandmarkToItinerary
    {
        public List<LandmarkModel> Landmarks { get; set; }
        public ItineraryModel Itinerary { get; set; }
    }
}