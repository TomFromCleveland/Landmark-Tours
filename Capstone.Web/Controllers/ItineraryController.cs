using Capstone.Web.DALs;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class ItineraryController : Controller
    {
        private IItineraryDAL itineraryDAL;

        public ItineraryController(IItineraryDAL itineraryDAL)
        {
            this.itineraryDAL = itineraryDAL;
        }
        // GET: Itinerary
        public ActionResult ViewItineraries( UserModel user)
        {
            return View("ViewItineraries", itineraryDAL.GetAllItineraries(user));
        }
    }
}