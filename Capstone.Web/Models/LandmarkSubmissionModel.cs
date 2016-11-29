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
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        [RegularExpression(@"\d{5}$", ErrorMessage = "Invalid Zip Code")]
        public string zip { get; set; }

        
        
    }
}