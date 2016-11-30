using Capstone.Web.DALs;
using Capstone.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Capstone.Web.Controllers
{
    public class LandmarkController : Controller
    {
        private ILandmarkDAL landmarkDAL;
        // GET: Landmark
        public LandmarkController(ILandmarkDAL landmarkDAL)
        {
            this.landmarkDAL = landmarkDAL;
        }

        public ActionResult LandmarkList()
        {
            return View("LandmarkList", landmarkDAL.GetAllApprovedLandmarks());
        }

        public ActionResult SubmitNewLandmark()
        {
            return View("SubmitNewLandmark", new LandmarkSubmissionModel());

        }

        [HttpPost]
        public ActionResult SubmissionConfirmation(LandmarkSubmissionModel lmc)
        {
            LandmarkModel landmark = RetrieveAddressCoordinates(lmc);
            landmarkDAL.SubmitNewLandmark(landmark);

            return View("SubmissionConfirmation");
        }

        public LandmarkModel RetrieveAddressCoordinates(LandmarkSubmissionModel lmc)
        {
            LandmarkModel landmark = new LandmarkModel();

            string address = lmc.StreetAddress + ", " + lmc.City + ", " + lmc.State + " " + lmc.zip;
            string requestUrl = string.Format("http://maps.googleapis.com/maps/api/geocode/json?address={0}&sensor=false", Uri.EscapeDataString(address));
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



                landmark.Latitude = geoData.results[0].geometry.location.lat.Value;
                landmark.Longitude = geoData.results[0].geometry.location.lng.Value;

            }

            landmark.Name = lmc.LandmarkName;
            string imgSrc = "https://maps.googleapis.com/maps/api/streetview?size=600x300&location=" + landmark.Latitude + "," + landmark.Longitude + "&heading=151.78&pitch=-0.76&key=" + "AIzaSyDu0VhcsrEx_f3CdQFVOC_Sw3r29lWBnYA";
            landmark.ImageName = imgSrc;
            landmark.Description = lmc.Description;
            return landmark;

        }

        public ActionResult UnapprovedLandmarkList()
        {
            return View("UnapprovedLandmarkList", landmarkDAL.GetAllUnapprovedLandmarks());
        }

        [HttpPost]
        public ActionResult ApproveLandmarks(List<LandmarkModel> landmarksList)
        {
            landmarkDAL.ApproveLandmarks(landmarksList);

            //Ask Josh: should this be a redirect? why or why not?
            ModelState.Clear();
            return View("UnapprovedLandmarkList", landmarkDAL.GetAllUnapprovedLandmarks());
        }
    }
}