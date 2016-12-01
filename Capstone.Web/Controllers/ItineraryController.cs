using Capstone.Web.DALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class ItineraryController : Controller
    {
        // GET: Itinerary
        public ActionResult ViewItineraries()
        {
            return View("ViewItineraries", ItineraryDAL.GetAllItineraries());
        }
    }
}