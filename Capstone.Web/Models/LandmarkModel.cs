using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class LandmarkModel
    {
        public int ID { get; set; }
                
        public string ImageName { get; set; }

        public bool IsApproved { get; set; } = false;

        [Required]
        public string Description { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public string GooglePlacesID { get; set; }

        public bool SubmissionSuccessful { get; set; } = false;
    }
}