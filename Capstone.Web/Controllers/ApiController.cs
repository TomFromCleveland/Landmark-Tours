using Capstone.Web.DALs;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class ApiController : BaseController
    {
        private IItineraryDAL itineraryDAL;
        private ILandmarkDAL landmarkDAL;

        public ApiController(IItineraryDAL itineraryDAL, ILandmarkDAL landmarkDAL, IUserDAL userDAL) : base(userDAL)
        {
            this.itineraryDAL = itineraryDAL;
            this.landmarkDAL = landmarkDAL;
        }

        [Route("api/add")]
        [HttpPost]
        public ActionResult LandmarkToItinerary(ItineraryIDLandmarkIDModel model)
        {

            if (itineraryDAL.AddItineraryLandmarks(model.LandmarkID, model.ItineraryID))
            {
                return Json(true);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

        }

        [Route("api/delete")]
        [HttpPost]
        public ActionResult RemoveLandmarkFromItinerary(ItineraryIDLandmarkIDModel model)
        {
            if (itineraryDAL.DeleteLandmarkFromItinerary(model.LandmarkID, model.ItineraryID))
            {
                return Json(true);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
    }
}