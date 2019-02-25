﻿using Fitnetium.Models;
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
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                // TODO: Add insert logic here
                var users = db.User.Where(u => u.ApplicationUserId == userLoggedin).FirstOrDefault();
               users.age =  user.age;
               users.weight =  user.weight;
               users.hieght =  user.hieght;
                users.category = user.category;
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
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
        public double CaloriesBurned(User user)
        {
            var METValues = db.MetValues.Where(m => m.Activities == user.WorkOutType.ToString()).Where(t=>t.Intensity == user.WorkOutType.ToString()).FirstOrDefault();
            var Met = Convert.ToDouble(METValues.Intensity);
            double energyExpenditure = .0175 * Met * (user.weight * 2.2f);
            return energyExpenditure;
        }
        public double[] CalHeratRateZones(User user) 
        {
            var maxHearRate = 220 - user.age;
            var restingHeart = maxHearRate - 80;


            float modifier=0f;
            if (user.WorkOutType == "Low")
            {
                modifier = .25f* restingHeart;

            }
            else if (user.WorkOutType == "Moderate")
            {
                modifier = .6f* restingHeart;
            }
            else if (user.WorkOutType == "Vigorous")
            {
                modifier = .8f* restingHeart;
            }
            var lowHHR = .25 * restingHeart+80;
            var highHHR = .8 * restingHeart + 80;
            double[] heartzone = new double[2];
            heartzone[0] = lowHHR;
            heartzone[1] = highHHR;
            return heartzone;
        }
        public string CalBMI(User user)
        {
            var BMI = (user.weight / ((65) ^ 2)) * 703;
            string weightStatus;
            if (BMI >= 30)
            {
                weightStatus = "Obese";
            }
            else if(BMI >=25 && BMI <=29.9)
            {
                weightStatus = "Overweight";
            }
            else if(BMI >=18.5 && BMI <=24.9)
            {
                weightStatus = "Normal";
            }
            else
            {
                weightStatus = "Underweight";
            }
            return weightStatus;
        }
        public async Task RecipeLookUp(string input,User user)
        {
            string API = "e3e0781f8229c421c4bc8a3293094f86";
            List<Recipe> recipes = new List<Recipe>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://api.edamam.com");
                var response = await client.GetAsync($"search?q={input}&app_id={API}&from0&to=3&calories=480-722&health=alcohol-free");
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
                        string fullName = json["hits"][i]["recipe"]["ingredients"][j]["text"].ToString();
                    }
                    var CookTime = json["hits"][i]["recipe"]["totaltime"].ToObject<float>();
                    var Calories = json["hits"][i]["recipe"]["calories"].ToObject<float>();

                    Recipe recipe = new Recipe()
                    {
                        RecipeName = Label,
                        Image = Image,
                        Ingredients = itemToSplit,
                        CookTime = CookTime,
                        Calories = Calories
                    };
                    recipes.Add(recipe);
                }
            }
        }
        public async Task<List<string[]>> GetWorkoutData()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercisecategory/"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int i = 0; i < json["results"].Count(); i++)
                    {
                        string[] temp = new string[2];
                        temp[0] = json["results"][i]["id"].ToString();
                        temp[1] = json["results"][i]["name"].ToString();
                        exercisecategory.Add(temp);
                    }
                    return exercisecategory;
                }
            }
         }
    }
}
