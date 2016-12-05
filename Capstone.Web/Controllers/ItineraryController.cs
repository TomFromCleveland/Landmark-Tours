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
        public ActionResult ViewItineraries()
        {
            int userID = 1; //TODO Make this (int)Session["UserID"];
            return View("ViewItineraries", itineraryDAL.GetAllItineraries(userID));
        }


        public ActionResult ItineraryDetails(int itineraryId)
        {
            return View("ItineraryDetails", itineraryDAL.GetItineraryDetails(itineraryId));
        }

        public ActionResult CreateItinerary()
        {
            ModelState.Clear();
            ItineraryModel itinerary =new ItineraryModel();
            //itinerary.UserID=;
            return View("CreateItinerary", itinerary);
        }

        [HttpPost]
        public ActionResult SubmitItinerary(ItineraryModel itinerary)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("CreateItinerary");
            }else
            {

                return RedirectToAction("Index","Home");
            }
        }



    }
}