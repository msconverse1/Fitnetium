using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitnetium.Models
{
    public class Workout
    {
        [Key]
        public int UserWorkoutID { get; set; }

        public string Name { get; set; }
        public string Equipent { get; set; }
        public string MainMuscles { get; set; }
        public string SencMuscles { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public float? Weight { get; set; }

    }
}