using Capstone.Web.DALs;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{

    public class ItineraryController : BaseController
    {
        private IItineraryDAL itineraryDAL;
        private ILandmarkDAL landmarkDAL;
        private IUserDAL userDAL;

        public ItineraryController(IItineraryDAL itineraryDAL, ILandmarkDAL landmarkDAL, IUserDAL userDAL) : base(userDAL)
        {
            this.itineraryDAL = itineraryDAL;
            this.landmarkDAL = landmarkDAL;
            this.userDAL=userDAL;
        }

        // GET: Itinerary
        public ActionResult ViewItineraries()
        {
            int userID = 1; //TODO Make this (int)Session["UserID"];
            return View("ViewItineraries", itineraryDAL.GetAllItineraries(userID));
        }

        public ActionResult ItineraryDetails(int itineraryID)
        {

            return View("ItineraryDetail", itineraryDAL.GetItineraryDetail(itineraryID));

        }

        public ActionResult CreateItinerary()
        {
            ModelState.Clear();
            ItineraryModel itinerary = new ItineraryModel();
            itinerary.UserID = userDAL.GetUser(CurrentUser).ID;
            return View("CreateItinerary", itinerary);
        }

        [HttpPost]
        public ActionResult SubmitItinerary(ItineraryModel itinerary)
        {
            if (!ModelState.IsValid)
            {

                return RedirectToAction("CreateItinerary");
            }
            else
            {
                ItineraryModel newItinerary = itineraryDAL.CreateNewItinerary(itinerary);
                return RedirectToAction("AddLandmarkToItinerary", new { id = newItinerary.ID });
            }
        }

        public ActionResult AddLandmarkToItinerary(int id)
        {
            List<LandmarkModel> landmarks = new List<LandmarkModel>();
            landmarks = landmarkDAL.GetAllApprovedLandmarks();
            AddLandmarkToItineraryViewModel landmarkAndItinerary = new AddLandmarkToItineraryViewModel();
            landmarkAndItinerary.Itinerary = itineraryDAL.GetItineraryByID(id);

            landmarkAndItinerary.AvailableLandmarks = landmarks;
            return View("AddLandmarkToItinerary", landmarkAndItinerary);
        }


    }
}