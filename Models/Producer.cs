using System;
using System.ComponentModel.DataAnnotations;
using eTicketing.Data.Base;

namespace eTicketing.Models
{
	public class Producer: IEntityBase
	{
        [Key]
        public int Id { get; set; }

        [Display(Name ="Profile Picture")]
        [Required(ErrorMessage ="Profile Picture is required")]
        public string ProfilePictureUrl { get; set; }

        [Display(Name ="Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Min Length 3 Max Lenght 50")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Bio is required")]
        public string Bio { get; set; }

        // Relationships
        public List<Movie>? Movies { get; set; }
    }
}

