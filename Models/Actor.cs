using System;
using System.ComponentModel.DataAnnotations;

namespace eTicketing.Models
{
	public class Actor
	{
        [Key]
        public int Id { get; set; }

        [Display(Name = "Profile Picture URL")]
        public string ProfilePictureUrl { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Biogrophy")]
        public string Bio { get; set; }

        // Relationships

        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}

