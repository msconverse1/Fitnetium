using Fitnetium.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Fitnetium.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: User
        public ActionResult Index(int? id )
        {
            return RedirectToAction("Index", "Workout", new { id });
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            var userLoggedin = User.Identity.GetUserId();
            var users = db.User.Where(u => u.ApplicationUserId == userLoggedin).FirstOrDefault();

            return View(users);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
              var userLoggedin = User.Identity.GetUserId();
             
               var users = db.User.Where(u => u.ApplicationUserId == userLoggedin).FirstOrDefault();
               users.age =  user.age;
               users.weight =  user.weight;
                
               users.hieght = (float)user.Heightset;
               user.hieght = (float)user.Heightset;
               users.Category = user.Category;
               double[] HeartRateZone = CalHeratRateZones(user);
               users.LowHHR = HeartRateZone[0];
               users.HighHHR = HeartRateZone[1];
               string[] BMI = CalBMI(user);
               users.BMIPercent = Convert.ToDouble(BMI[0]);
               users.BMIType = BMI[1];
               db.SaveChanges();
               return RedirectToAction("Index","Workout");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,User user)
        {
            try
            {
                var userLoggedin = User.Identity.GetUserId();

                // TODO: Add update logic here
                var users = db.User.Where(u => u.ApplicationUserId == userLoggedin).FirstOrDefault();
                
                users.age = user.age;
                users.weight = user.weight;
                users.hieght = (float)user.Heightset;
                user.hieght = (float)user.Heightset;
                users.Category = user.Category;
                double[] HeartRateZone = CalHeratRateZones(user);
                users.LowHHR = HeartRateZone[0];
                users.HighHHR = HeartRateZone[1];
                string[] BMI = CalBMI(user);
                users.BMIPercent = Convert.ToDouble(BMI[0]);
                users.BMIType = BMI[1];

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public double[] CalHeratRateZones(User user) 
        {
            var maxHearRate = 220 - user.age;
            var restingHeart = maxHearRate - 80;


            float modifier=0f;
            if (user.Category.ToString() == "Low")
            {
                modifier = .25f* restingHeart;

            }
            else if (user.Category.ToString() == "Moderate")
            {
                modifier = .6f* restingHeart;
            }
            else if (user.Category.ToString() == "Vigorous")
            {
                modifier = .8f* restingHeart;
            }
            var lowHHR = .25 * restingHeart+80;
            var highHHR = modifier * restingHeart + 80;
            double[] heartzone = new double[2];
            heartzone[0] = lowHHR;
            heartzone[1] = highHHR;
            return heartzone;
        }
        public string[] CalBMI(User user)
        {
            string[] BMI = new string[2];
            BMI[0] = (((user.weight / user.hieght) /user.hieght) * 703).ToString();
            
            if (Convert.ToDouble(BMI[0]) >= 30)
            {
                BMI[1] = "Obese";
            }
            else if(Convert.ToDouble(BMI[0]) >= 25 && Convert.ToDouble(BMI[0]) <= 29.9)
            {
                BMI[1] = "Overweight";
            }
            else if(Convert.ToDouble(BMI[0]) >= 18.5 && Convert.ToDouble(BMI[0]) <= 24.9)
            {
                BMI[1] = "Normal";
            }
            else
            {
                BMI[1] = "Underweight";
            }
            return BMI;
        }
        public async Task<ActionResult> CheckForRecipe(int id, string date, double calories)
        {
            var recipe = db.Recipes.Where(r => r.WorkoutID == id).Where(d => d.Day == date).ToList();

            if (recipe.Count == 0)
            {
             return  await RecipeLookUp(id, date, calories);
          
            }
            else
            {
                return View("MealsToEat", recipe);
            }
            
        }
        public async Task<ActionResult> RecipeLookUp(int id,string date,double calories)
        {
            var users = db.User.Where(i=>i.ID == id).FirstOrDefault();
            var day = db.Mondays.Where(d=>d.DayOfWeek == date).Where(w=>w.WorkoutID==id).FirstOrDefault();
            Random random = new Random();
            var num = random.Next(7);
            string food="";
            switch (num)
            {
                case 0:
                    food = "";
                    break;
                case 1:
                    food = "beef";
                    break;
                case 2:
                    food = "chicken";
                    break;
                case 3:
                    food = "pork";
                    break;
                case 4:
                    food = "beef";
                    break;
                case 5:
                    food = "";
                    break;
                case 6:
                    food = "pork";
                    break;
                case 7:
                    food = "chicken";
                    break;
                default:
                    break;
            }
            string API = "e3e0781f8229c421c4bc8a3293094f86";
            string app_id = "45a6dda3"; 
            List<Recipe> recipes = new List<Recipe>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.edamam.com");
                var response = await client.GetAsync($"search?q={food}&app_id={app_id}&app_key={API}&from0&to=3&calories={calories}&health=alcohol-free");
                response.EnsureSuccessStatusCode();

                var stringResult = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(stringResult);
               
                for (int i = 0; i < 3; i++)
                {
                    var Label = json["hits"][i]["recipe"]["label"].ToString();
                    var Image = json["hits"][i]["recipe"]["image"].ToString();
                    var IngredCount = json["hits"][i]["recipe"]["ingredients"].Count();
                    string[] itemToSplit = new string[IngredCount];
                    for (int j = 0; j < IngredCount; j++)
                    {
                        itemToSplit[j] = json["hits"][i]["recipe"]["ingredients"][j]["text"].ToString();
                    }
                    var CookTime = json["hits"][i]["recipe"]["totalTime"].ToObject<float>();
                    var Calories = json["hits"][i]["recipe"]["calories"].ToObject<float>();

                    Recipe recipe = new Recipe()
                    {
                        RecipeName = Label,
                        Image = Image,
                        Ingredients = itemToSplit,
                        CookTime = CookTime,
                        Calories = Calories,
                        WorkoutID = day.WorkoutID,
                        Day = day.DayOfWeek
                    };
                    db.Recipes.Add(recipe);
                    recipes.Add(recipe);
                    db.SaveChanges();
                }
            }
            return View("MealsToEat", recipes);
        }
        public ActionResult DetailsToWorkout(int id)
        {
            return RedirectToAction("Details", "Workout", new { id });
        }

        public ActionResult Badge()
        {
            return View();
        }
    }
}
