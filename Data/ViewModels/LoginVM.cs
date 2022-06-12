using System;
using System.ComponentModel.DataAnnotations;

namespace eTicketing.Data.ViewModels
{
	public class LoginVM
	{
        [Display(Name ="Email Address")]
        [Required(ErrorMessage ="Email Address is Required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

