using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class LandmarkSubmissionModel
    {
        [Required]
        public string LandmarkName { get; set; }
        [Required]
        public string Description { get;set;}
        [Required]
        public string Address { get; set; }
        [Required]
        public string GooglePlacesID { get; set; }

        public bool SubmissionSuccessful { get; set; } = false;

        
        
    }
}