using Capstone.Web.DALs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Capstone.Web.Controllers
{
    public class ReviewController : BaseController
    {
        private IReviewDAL reviewDAL;

        public ReviewController(IReviewDAL reviewDAL, IUserDAL userDAL) : base(userDAL)
        {
            this.reviewDAL = reviewDAL;
        }



    }
}