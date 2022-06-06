using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using eTicketing.Data;
using eTicketing.Data.Base;

namespace eTicketing.Models
{
	public class Movie: IEntityBase
	{
        [Key]
        public int Id { get; set; }

        [Display(Name="Movie Name")]
        public string Name { get; set; }

        [Display(Name = "Movie Description")]
        public string Description { get; set; }

        [Display(Name = "Movie Price")]
        public double Price { get; set; }

        [Display(Name = "Movie ImageURL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndTime { get; set; }

        public MovieCategory MovieCategory { get; set; }

        // Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }

        // Cinema
        public int CinemaId { get; set; }
        [ForeignKey("CinemaId")]

        public Cinema Cinema { get; set; }

        // Producer
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]

        public Producer Producer { get; set; }

    }
}

