using Capstone.Web.DALs;
using Capstone.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            this.userDAL = userDAL;
        }

        // GET: Itinerary
        public ActionResult ViewItineraries()
        {
            int userID = 1; //TODO Make this (int)Session["UserID"];
            return View("ViewItineraries", itineraryDAL.GetAllItineraries(userID));
        }

        public ActionResult ItineraryDetails(int itineraryID)
        {
            ItineraryModel itinerary = new ItineraryModel();

            itinerary = itineraryDAL.GetItineraryDetail(itineraryID);
            GetDirections(itinerary);
            return View("ItineraryDetail", itinerary);

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

        public void GetDirections(ItineraryModel itinerary)
        {

            
            string requestUrl = "https://maps.googleapis.com/maps/api/directions/json?origin=";
            requestUrl = requestUrl + itinerary.StartingLatitude + "," + itinerary.StartingLongitude;
            requestUrl = requestUrl + "&destination=" + itinerary.LandmarkList[itinerary.LandmarkList.Count - 1].Latitude + ",";
            requestUrl = requestUrl + itinerary.LandmarkList[itinerary.LandmarkList.Count - 1].Longitude;
            requestUrl = requestUrl + "&waypoints=optimize:true";
            foreach (LandmarkModel landmark in itinerary.LandmarkList)
            {
                requestUrl = requestUrl + "|" + landmark.Latitude + "," + landmark.Longitude;
            }
            requestUrl = requestUrl + "&key=AIzaSyDu0VhcsrEx_f3CdQFVOC_Sw3r29lWBnYA";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = "Get";
            WebResponse response = request.GetResponse();

            if (response != null)
            {
                string str = null;
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader streamReader = new StreamReader(stream))
                    {
                        str = streamReader.ReadToEnd();
                    }
                }

                var geoData = JsonConvert.DeserializeObject<dynamic>(str);
                

            }
        }


       
    }
}