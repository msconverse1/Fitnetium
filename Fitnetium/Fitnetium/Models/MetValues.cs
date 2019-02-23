using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fitnetium.Models
{
    public class MetValues
    {
        [Key]
        public int ID { get; set; }
        public string Activities {get;set;}
        public float MET { get; set; }
        public string Intensity { get; set; }
    }
}