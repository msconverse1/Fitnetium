﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fitnetium.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int age { get; set; }
        public float weight { get; set; }
        public float hieght { get; set; }
        [ForeignKey("Workout")]
        [Display(Name ="WorkoutID")]
        public int? UserWorkoutID { get; set; }
        public Workout Workout { get; set; }

        [ForeignKey("ApplicationUser")]
        [Display(Name = "AppID")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}