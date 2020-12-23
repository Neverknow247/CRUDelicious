using System;
using System.ComponentModel.DataAnnotations;
namespace CRUDelicious.Models
{
    public class Dishes
    {
        [Key]
        public int DishId {get;set;}
        [Required]
        [MinLength(2)]
        [Display(Name = "Name of Dish")]
        public string Name {get;set;}
        [Required]
        [MinLength(2)]
        [Display(Name = "Chef's Name")]
        public string Chef {get;set;}
        public int Tastiness {get;set;}
        [Required]
        [Range(1,25000)]
        [Display(Name = "# of Calories")]
        public int Calories {get;set;}
        [Required]
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}