using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ItineraryModel
    {
        public int ID { get; set; }
        public double StartingLatitude { get; set; }
        public double StartingLongitude { get; set; }
        public List<LandmarkModel> LandmarkList { get; set; } //List of landmarks within an itinerary
        public int UserID { get; set; }
        public string Name { get; set; }  //Name of itinerary
        public DateTime Date { get; set; }
    }
}