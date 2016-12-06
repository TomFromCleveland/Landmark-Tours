using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class CreateUserViewModel
    {

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "User Name:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Password:")]

        //TODO: Make mroe reg expression for password validations

        [RegularExpression(@"(?=^.{8,}$)(?=.*\d)(?=.*[!@#$%^&*]+)(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "You need a stronger password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password:")]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; } = "";
    }
}