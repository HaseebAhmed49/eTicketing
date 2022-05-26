using System;
using System.ComponentModel.DataAnnotations;

namespace eTicketing.Models
{
	public class Actor
	{
        [Key]
        public int Id { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string FullName { get; set; }

        public string Bio { get; set; }
    }
}

