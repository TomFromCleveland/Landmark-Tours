using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class UserModel
    {
        public List<ItineraryModel> Itineraries { get; set; }
        public int ID { get; set; } 
    }
}