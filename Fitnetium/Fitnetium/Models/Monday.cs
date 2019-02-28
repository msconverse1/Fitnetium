using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fitnetium.Models
{
    public class Monday
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }
        public double? Met { get; set; }
        public double? CaloriesBurned { get; set; }
        public float? Weight { get; set; }
        [ForeignKey("Workout")]
        [Display(Name = "WorkoutID")]
        public int? WorkoutID { get; set; }
        public Workout Workout { get; set; }
        public double? Time { get; set; }
        public int? RepsCompleted { get; set; }
        public int? SetsCompleted { get; set; }
        public double? ActualCalories { get; set; }
        public double? TimeCompleted { get; set; }
        public string DayOfWeek { get; set; }
        public DateTime Date { get; set; }
    }
}