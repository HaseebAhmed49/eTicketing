using System;
using System.ComponentModel.DataAnnotations;

namespace eTicketing.Models
{
	public class Actor
	{
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage ="Profile Picture is Required")]
        public string ProfilePictureUrl { get; set; }

        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is Required")]
        [StringLength(50, MinimumLength =3,ErrorMessage ="Full Name lenght must be between min 3 and max 50")]
        public string FullName { get; set; }

        [Display(Name = "Biogrophy")]
        [Required(ErrorMessage = "Bio is Required")]
        public string Bio { get; set; }

        // Relationships
        public List<Actor_Movie>? Actors_Movies { get; set; }
    }
}

