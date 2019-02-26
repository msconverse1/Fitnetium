﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fitnetium.Models
{
    public class Workout
    {
        [Key]
        public int? UserWorkoutID { get; set; }
        public enum WorkoutType
        {
            [Description("Abs")]
            Abs = 10,
            [Description("Arms")]
            Arms=8,
            [Description("Back")]
            Back=12,
            [Description("Calves")]
            Calves=14,
            [Description("Chest")]
            Chest=11,
            [Description("Leg")]
            Leg=9,
            [Description("Shoulders")]
            Shoulders=13

        }
        [Display(Name ="Day One Workout")]
        public WorkoutType Category { get; set; }
        [Display(Name ="Name of Workout")]
        public string Name { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [ForeignKey("User")]
        [Display(Name = "UserID")]
        public int? UserID { get; set; }
        public User User { get; set; }
    }
}