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
                return RedirectToAction("Index", "Home");

            }
            else
            {
                return View("Login", model);
            }
        }
        
            [HttpGet]
            public ActionResult CreateUser()
        {
            if (base.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home", new { username = base.CurrentUser });
            }
            else
            {
                CreateUserViewModel newUser = new CreateUserViewModel();
                return View("CreateUser", newUser);
            }
            
        }

        [HttpPost]
        public ActionResult CreateUser(CreateUserViewModel viewModel)
        {
            if (base.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var currentUser = userDAL.GetUser(viewModel.Username);

                if (currentUser != null)
                {
                    viewModel.ErrorMessage = "This username is already taken.";
                    return View("CreateUser", viewModel);
                }

                var hashProvider = new HashProvider();
                var hashedPassword = hashProvider.HashPassword(viewModel.Password);
                var salt = hashProvider.SaltValue;

                var newUser = new UserModel
                {
                    Username = viewModel.Username,
                    Password = hashedPassword,
                    Salt = salt
                };

                userDAL.CreateNewUser(newUser);

                base.LogUserIn(viewModel.Username);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("CreateUser", viewModel);
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            base.LogUserOut();

            return RedirectToAction("Index", "Home");
        }
    }
}