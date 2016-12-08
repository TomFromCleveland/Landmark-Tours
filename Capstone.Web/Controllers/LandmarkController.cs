using Capstone.Web.DALs;
using Capstone.Web.Filters;
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
    public class LandmarkController : BaseController
    {
        private ILandmarkDAL landmarkDAL;
        private IUserDAL userDAL;
        // GET: Landmark
        public LandmarkController(ILandmarkDAL landmarkDAL, IUserDAL userDAL) : base(userDAL)
        {
            this.landmarkDAL = landmarkDAL;
            this.userDAL = userDAL;
        }

        public ActionResult LandmarkList()
        {
            return View("LandmarkList", landmarkDAL.GetAllApprovedLandmarks());
        }

        public ActionResult LandmarkDetail(int landmarkID)
        {
            //TODO Add actual reviews, get rid of dummy review in View, format reviews
            LandmarkModel landmark = landmarkDAL.GetLandmark(landmarkID);
            return View("LandmarkDetail", landmark);
        }


        public ActionResult SubmitNewLandmark()
        {
            return View("SubmitNewLandmark", new LandmarkModel());

        }

        [HttpPost]
        public ActionResult SubmissionConfirmation(LandmarkModel landmark)
        {
            string imgSrc = "https://maps.googleapis.com/maps/api/streetview?size=600x300&location=" + landmark.Latitude + "," + landmark.Longitude + "&heading=151.78&pitch=-0.76&key=" + "AIzaSyDu0VhcsrEx_f3CdQFVOC_Sw3r29lWBnYA";
            landmark.ImageName = imgSrc;

            landmark.SubmissionSuccessful = landmarkDAL.SubmitNewLandmark(landmark);
            return View("SubmissionConfirmation", landmark);
        }


        public ActionResult UnapprovedLandmarkList()
        {
            if ((string)Session["username"] != null)
            {


                bool admin = userDAL.GetUser((string)Session["username"]).IsAdmin;
                if (admin)
                {
                    return View("UnapprovedLandmarkList", landmarkDAL.GetAllUnapprovedLandmarks());
                }
                else
                {
                    return new HttpStatusCodeResult(401);
                }
            }
            else
            {
                
                return new HttpStatusCodeResult(401);
            }

        }

        [HttpPost]
        public ActionResult ApproveLandmarks(List<LandmarkModel> landmarksList)
        {

            if ((string)Session["username"] != null)
            {
                landmarkDAL.ApproveLandmarks(landmarksList);

                //Ask Josh: should this be a redirect? why or why not?
                ModelState.Clear();
                bool admin = userDAL.GetUser((string)Session["username"]).IsAdmin;
                if (admin)
                {


                    return View("UnapprovedLandmarkList", landmarkDAL.GetAllUnapprovedLandmarks());
                }
                else
                {
                    return new HttpStatusCodeResult(401);
                }

            }
            else
            {
                return new HttpStatusCodeResult(401);
            }
        }
    }
}