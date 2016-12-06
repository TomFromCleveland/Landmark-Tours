using Capstone.Web.DALs;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class BaseController : Controller
    {
        private const string UsernameKey = "username";
        private readonly IUserDAL userDAL;

        public BaseController(IUserDAL userDAL)
        {
            this.userDAL = userDAL;
        }

        public string CurrentUser
        {
            get
            {
                string username = string.Empty;

                //Check to see if user cookie exists, if not create it
                if (Session[UsernameKey] != null)
                {
                    username = (string)Session[UsernameKey];
                }

                return username;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return Session[UsernameKey] != null;
            }
        }

        public void LogUserIn(string username)
        {
            Session[UsernameKey] = username;
        }

        public void LogUserOut()
        {
            Session.Abandon();
        }

        [ChildActionOnly]
        public ActionResult GetAuthenticatedUser()
        {
            UserModel user = null;

            if (IsAuthenticated)
            {
                user = userDAL.GetUser(CurrentUser);
            }
            //TODO Rami is working on _AuthenticationBar
            return View("_AuthenticationBar", user);
        }
    }
}