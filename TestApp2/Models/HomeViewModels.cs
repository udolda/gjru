using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestApp2.Models
{
    public class HomeViewModels
    {

    }


    public class RegistrationViewModel
    {
        public RegistrationViewModel()
        {
            Role = role.None;
        }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "passwordConfirm")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string passwordConfirm { get; set; }

        [Required]
        public role Role { get; set; }
    }


    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}