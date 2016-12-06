using Capstone.Web.Crypto;
using Capstone.Web.DALs;
using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserDAL userDAL;

        public UserController(IUserDAL userDAL) : base(userDAL)
        {
            this.userDAL = userDAL;
        }



        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            if (base.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { username = base.CurrentUser });
            }
            LoginViewModel model = new LoginViewModel();
            return View("Login", model);
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserModel user = userDAL.GetUser(model.Username);
                if (user == null)
                {
                    model.ErrorMessage = "The username or password is invalid";
                    return View("Login", model);
                }
                HashProvider hashProvider = new HashProvider();
                if (!hashProvider.VerifyPasswordMatch(user.Password, model.Password, user.Salt))
                {
                    model.ErrorMessage = "The username or password is invalid";
                    return View("Login", model);
                }
                base.LogUserIn(user.Username);
                return RedirectToAction("Index", "Home", new { username = base.CurrentUser });

            }
            else
            {
                return View("Login", model);
            }
        }




        //Login()
        //Login(LoginViewModel??)[HttpPost] - actually logs user in, calls hash provider, hashes password

        //CreateNewUser()
        //CreateNewUser(NewUserViewModel??)[HttpPost] - writes new user to DB
        [HttpGet]
        [Route("logout")]
        public ActionResult Logout()
        {
            base.LogUserOut();

            return RedirectToAction("Index", "Home");
        }

    }
}