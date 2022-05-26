using System;
using System.ComponentModel.DataAnnotations;

namespace eTicketing.Models
{
	public class Movie
	{
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndTime { get; set; }

        public MovieCategory MovieCategory { get; set; }
    }
}

