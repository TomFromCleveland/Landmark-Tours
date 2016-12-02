using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Name { get; set; }  //Name of itinerary
        [Required]
        [DataType(DataType.Date)]
        [CurrentDate(ErrorMessage ="Date must be today or after.")]
        public DateTime Date { get; set; }
    }
}

