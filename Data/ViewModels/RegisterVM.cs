using System;
using System.ComponentModel.DataAnnotations;

namespace eTicketing.Data.ViewModels
{
	public class RegisterVM
	{
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is Required")]
        public string FullName { get; set; }

        [Display(Name ="Email Address")]
        [Required(ErrorMessage ="Email Address is Required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name ="Confirm Password")]
        [Required(ErrorMessage ="Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password don't match")]
        public string ConfirmPassword { get; set; }
    }
}