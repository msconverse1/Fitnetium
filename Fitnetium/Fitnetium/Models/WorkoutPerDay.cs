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
       public List<Monday> Tuesday { get; set; }
       public List<Monday> Wednesday { get; set; }
       public List<Monday> Thursday { get; set; }
       public List<Monday> Friday { get; set; }
       public List<Monday> Saturday { get; set; }
       public List<Monday> Sunday { get; set; }

    }
}