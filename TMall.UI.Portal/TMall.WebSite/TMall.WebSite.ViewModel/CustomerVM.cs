using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TMall.WebSite.ViewModel
{
    //[FluentValidation.Attributes.Validator(typeof(CustomerModelValidator))]
    public class CustomerVM
    {
        [Display(Name = "User name")]
        public string CustomerName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("PasswordHash", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Email address")]
        public string Email { get; set; }
    }
}
