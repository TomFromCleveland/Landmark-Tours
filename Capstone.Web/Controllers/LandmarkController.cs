using Capstone.Web.DALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Index()
        {
            return View();
        }
    }
}