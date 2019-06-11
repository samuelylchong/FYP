using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcFYP.Models
{
    [MetadataType(typeof(User.Metadata))]
    public partial class User
    {
        sealed class Metadata
        {
            [Display(Name = "User Name")]
            [Required(ErrorMessage ="*This field is required.")]
            public string Username { get; set; }

            [DataType(DataType.Password)]
            [Required(ErrorMessage = "*This field is required.")]
            public string Password { get; set; }

            [EmailAddress(ErrorMessage = "Invalid Email Address")]
            [Required(ErrorMessage = "*This field is required.")]
            public string Email { get; set; }
        }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*This field is required.")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string LoginErrorMessage { get; set; }
    }
}