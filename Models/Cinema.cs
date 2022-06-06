using System;
using System.ComponentModel.DataAnnotations;
using eTicketing.Data.Base;

namespace eTicketing.Models
{
    public class Cinema:IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage ="Logo Image is required")]
        public string Logo { get; set; }

        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Cinema name is required")]
        [StringLength(30,MinimumLength =10,ErrorMessage ="Cinema Name should be Minimum 10 or Maximum 30 chars")]
        public string Name { get; set; }

        [Display(Name="Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        // Relationships
        public List<Movie>? Movies { get; set; }
    }
}

