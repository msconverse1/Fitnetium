using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Fitnetium.Models
{
    public class Thursday
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Sets { get; set; }
        public int? Reps { get; set; }

        public float? Weight { get; set; }
        [ForeignKey("User")]
        [Display(Name = "UserID")]
        public int? UserID { get; set; }
        public User User { get; set; }
    }
}