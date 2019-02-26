using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fitnetium.Models
{
    public class WorkoutPerDay
    {
       public Workout Workout { get; set; }
       public List<Monday> Monday { get; set; }
       public List<Tuesday>Tuesday { get; set; }
       public List<Wednesday> Wednesday { get; set; }
       public List<Thursday> Thursday { get; set; }
       public List<Friday> Friday { get; set; }
       public List<Saturday> Saturday { get; set; }
       public List<Sunday> Sunday { get; set; }

    }
}