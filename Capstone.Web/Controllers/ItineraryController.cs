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
        private ILandmarkDAL landmarkDAL;

        public ItineraryController(IItineraryDAL itineraryDAL, ILandmarkDAL landmarkDAL)
        {
            this.itineraryDAL = itineraryDAL;
            this.landmarkDAL = landmarkDAL;
        }

        // GET: Itinerary
        public ActionResult ViewItineraries()
        {
            int userID = 1; //TODO Make this (int)Session["UserID"];
            return View("ViewItineraries", itineraryDAL.GetAllItineraries(userID));
        }


        //public ActionResult ItineraryDetail(int itineraryID)
        //{
        //    return View("ItineraryDetail", itineraryDAL.GetItineraryDetail(itineraryID);
        //}

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
                List<LandmarkModel> landmarks = new List<LandmarkModel>();
                    landmarks=landmarkDAL.GetAllApprovedLandmarks();
                AddLandmarkToItinerary landmarkAndItinerary = new AddLandmarkToItinerary();
                landmarkAndItinerary.Itinerary = itinerary;
                landmarkAndItinerary.Landmarks = landmarks;
                return View("AddLandmarkToItinerary", landmarkAndItinerary);
            }
        }



    }
}