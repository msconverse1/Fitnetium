using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}