using Capstone.Web.Crypto;
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
        [RegExContainsNumber("/(?=.\\d)/", ErrorMessage = "Password must contain a number.")]
        [RegExContainsLowercase("/(?=.[a-z])/", ErrorMessage = "Password must contain a lowercase letter.")]
        [RegExContainsUppercase("/(?=.[A-Z])/", ErrorMessage = "Password must contain a capital letter.")]
        [RegExNoWhiteSpace("/(?!.\\s)/", ErrorMessage = "Password cannot contain whitespace.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password:")]
        public string ConfirmPassword { get; set; }

        public string ErrorMessage { get; set; } = "";
    }
}