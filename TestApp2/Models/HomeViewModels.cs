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


    public class RegistrationViewModel : EntityModel<User>
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

        //[Display(Name = "Ваш номер телефона")]
        //public long PhoneNumber { get; set; }

        [Display(Name ="UserCompany")]
        public string UserCompany { get; set; }

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


    public class UserListViewModel : EntityModel<List<User>>
    {
        public IList<User> Users { get; set; }
        public UserListViewModel()
        {
            Users = new List<User>();
        }
    }
}