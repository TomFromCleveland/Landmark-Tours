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

        //TODO add validation for username/password
        public string Password { get; set; }
        public string Username { get; set; }
        public string Salt { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}