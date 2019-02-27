using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fitnetium.Models
{
    public class Saturday
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }

        public float? Weight { get; set; }
        [ForeignKey("Workout")]
        [Display(Name = "WorkoutID")]
        public int? WorkoutID { get; set; }
        public Workout Workout { get; set; }
        public double CaloriesBurned { get; internal set; }
        public int Met { get; internal set; }
    }
}