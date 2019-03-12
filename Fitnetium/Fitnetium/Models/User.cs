using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public enum Difficulty
        {
            [Description("Low")]
            Low,
            [Description("Moderate")]
            Moderate,
            [Description("Vigorous")]
            Back,
        }

        public enum Height
        {
            [Description("5,0")]    
            Five = 60,
            [Description("5,1")]
            FiveOne = 61,
            [Description("5,2")]
            FiveTwo= 62,
            [Description("5,3")]
            FiveThree= 63,
            [Description("5,4")]
            FiveFour=  64,
            [Description("5,5")]
           FiveFive= 65,
            [Description("5,6")]
            FiveSix= 66,
            [Description("5,7")]
            FiveSeven =67,
            [Description("5,8")]
            FiveEight =68,
            [Description("5,9")]
            FiveNine= 69,
            [Description("5,10")]
            FiveTen=70,
            [Description("5,11")]
            FiveEleven=71,
            [Description("6,0")]
            Six=72,
            [Description("6,1")]
            SixOne=73,
            [Description("6,2")]
            SixTwo=74,
            [Description("6,3")]
            SixThree=75,
            [Description("6,4")]
            SixFour=76,
            [Description("6,5")]
            SixFive=77,
            [Description("6,6")]
            SixSix=78,
            [Description("6,7")]
            SixSeven=79,
            [Description("6,8")]
            SixEight=80,
            [Description("6,9")]
            SixNine=81,
            [Description("6,10")]
            SixTen=82,
            [Description("6,11")]
            SixEleven=83,
            [Description("7,0")]
            Seven=84,
            [Description("7,1")]
            SevenOne=85,
            [Description("7,2")]
            SevenTwo=86
        }
        public Difficulty Category { get; set; }
        public string WorkOutType { get; set; }
        public double LowHHR { get; set; }
        public double HighHHR { get; set; }

        public double BMIPercent { get; set; }
        public string BMIType { get; set; }
        public int age { get; set; }
        public float weight { get; set; }
        public Height Heightset { get; set; }
        public float hieght { get;set; }
        public float CaloriesBurned { get; set; }

        [ForeignKey("ApplicationUser")]
        [Display(Name = "AppID")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}