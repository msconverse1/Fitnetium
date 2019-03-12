using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fitnetium.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeID { get; set; }
        public string RecipeName { get; set; }
        public string Image { get; set; }
        public string[] Ingredients { get; set; }
        public float CookTime { get; set; }
        public float Calories { get; set; }
        public string Day { get; set; }
        [ForeignKey("Workout")]
        [Display(Name = "WorkoutID")]
        public int? WorkoutID { get; set; }
        public Workout Workout { get; set; }
    }
}