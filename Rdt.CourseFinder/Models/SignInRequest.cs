using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Rdt.CourseFinder.Models
{
    public class SignInRequest
    {
        [Display(Name = "Email")]
        [Required]
        //[EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Keep me logged in")]
        public bool IsPresistant { get; set; }

        public SignInRequest()
        {
            IsPresistant = true;
        }
    }
}