using Capstone.Web.DALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUserDAL userDAL;

        public HomeController(IUserDAL userDAL) : base(userDAL)
        {
            this.userDAL = userDAL;
        }

        // GET: Home
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}