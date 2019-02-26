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
    public class WorkoutController : Controller
    {
        ApplicationDbContext db;
        public WorkoutController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Workout
        public ActionResult Index()
        {

            var workouts = db.Workouts.ToList();

            return View(workouts);
        }

        // GET: Workout/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Workout/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workout/Create
        [HttpPost]
        async public Task<ActionResult> Create(Workout workout)
        {
            try
            {
                var userLoggedin = User.Identity.GetUserId();
             var test=  await GetWorkoutData(workout);
                List<JToken> workoutName = new List<JToken>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome"&& names != "Test" && names != "What")
                    {
                        workoutName.Add(names);

                    }
                }
                var users = db.User.Where(u => u.ApplicationUserId == userLoggedin).FirstOrDefault();
                workout.UserID = users.ID;
                workout.User = users;
                db.Workouts.Add(workout);
                db.SaveChanges();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Workout/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Workout/Edit/5
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

        // GET: Workout/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Workout/Delete/5
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
        public async Task<List<JToken>> GetWorkoutData(Workout workout)
        {
            using (var httpClient = new HttpClient())
            {
                List<JToken> Holder = new List<JToken>();


                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=1"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                        var response = await httpClient.SendAsync(request);
                        var stringResult = await response.Content.ReadAsStringAsync();
                        var json = JObject.Parse(stringResult);
                        List<string[]> exercisecategory = new List<string[]>();
                        
                        for (int j  = 0; j < json["results"].Count(); j++)
                        {
                            var temp = Convert.ToInt32(json["results"][j]["category"]);
                            var lang = Convert.ToInt32(json["results"][j]["language"]);
                            if (temp == (int)workout.Category && lang == 2)
                            {
                                Holder.Add(json["results"][j]);
                            }

                        }
                    }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=2"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=3"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=4"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=5"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=6"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=7"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=8"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=9"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=10"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=11"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=12"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=13"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=14"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=15"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=16"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=17"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=18"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=19"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=20"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=21"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=22"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=23"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=24"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=25"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=26"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=27"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=28"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://wger.de/api/v2/exercise/?page=29"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Token c86e854254b62db27cfcc352eb8ead5128456581");

                    var response = await httpClient.SendAsync(request);
                    var stringResult = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(stringResult);
                    List<string[]> exercisecategory = new List<string[]>();

                    for (int j = 0; j < json["results"].Count(); j++)
                    {
                        var temp = Convert.ToInt32(json["results"][j]["category"]);
                        var lang = Convert.ToInt32(json["results"][j]["language"]);
                        if (temp == (int)workout.Category && lang == 2)
                        {
                            Holder.Add(json["results"][j]);
                        }

                    }
                }

                return Holder;
            }
        }
    }
}
