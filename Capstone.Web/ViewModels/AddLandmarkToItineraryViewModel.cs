using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class AddLandmarkToItineraryViewModel
    {
        public List<LandmarkModel> AvailableLandmarks { get; set; }
        public ItineraryModel Itinerary { get; set; }
    }
}