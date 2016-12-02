using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ReviewModel
    {
        public int ID { get; set; }
        public int LandmarkID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public bool ThumbsUp { get; set; } = false;
        public bool ThumbsDown { get; set; } = false;
    }
}