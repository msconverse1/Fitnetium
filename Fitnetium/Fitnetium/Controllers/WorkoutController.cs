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
            var userLoggedin = User.Identity.GetUserId();
            var users = db.User.Where(u => u.ApplicationUserId == userLoggedin).FirstOrDefault();

            var workouts = db.Workouts.Where(w=>w.UserID == users.ID).ToList();

            return View(workouts);
        }

        // GET: Workout/Details/5
        public ActionResult Details(int id)
        {
            var workout = db.Workouts.Where(d => d.UserWorkoutID == id).SingleOrDefault();

            WorkoutPerDay workoutPerDay = new WorkoutPerDay
            {
                Workout = workout,
                Monday = db.Mondays.Where(m => m.WorkoutID == workout.UserWorkoutID).ToList(),
                Tuesday = db.Tuesdays.Where(m => m.WorkoutID == workout.UserWorkoutID).ToList(),
                Wednesday = db.Wednesdays.Where(m => m.WorkoutID == workout.UserWorkoutID).ToList(),
                Thursday = db.Thursdays.Where(m => m.WorkoutID == workout.UserWorkoutID).ToList(),
                Friday = db.Fridays.Where(m => m.WorkoutID == workout.UserWorkoutID).ToList(),
                Saturday = db.Saturdays.Where(m => m.WorkoutID == workout.UserWorkoutID).ToList(),
                Sunday = db.Sundays.Where(m => m.WorkoutID == workout.UserWorkoutID).ToList(),
            };

            return View(workoutPerDay);
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
                var users = db.User.Where(u => u.ApplicationUserId == userLoggedin).FirstOrDefault();
                workout.UserID = users.ID;
                workout.User = users;
                db.Workouts.Add(workout);
                db.SaveChanges();
                await Mondayworkout(users,workout);
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
        
        public ActionResult CaloriesBurnedMonday(int? id)
        {
            var model = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            return View(model);
        }
        public ActionResult VisualizeCaloriesBurnedMonday(int? id)
        {
 

            var works = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();

            return Json(List(works.UserWorkoutID), JsonRequestBehavior.AllowGet);

        }
        public List<Monday> List(int? id)
        {
            List<Monday> mondays = new List<Monday>();

            mondays = db.Mondays.Where(m => m.WorkoutID == id).ToList();
            return mondays;
        }

    public double CaloriesBurned(User user,int mets)
    {
        double energyExpenditure = .0175 * mets * (user.weight * 2.2f);
        return energyExpenditure;
    }
        //get list of workouts
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
        //list of daliy workouts
     async public Task Mondayworkout(User users,Workout workout)
        {
            Monday monday = new Monday();
            Random random = new Random();
            if (users.Category.ToString() == "Low")
            {              
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                int temp = 0;
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }

                    }
                }               
                for (int i = 0; i < 4; i++)
                {
                    int ran;
                    do
                    {
                         ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    
                    monday.Name = workoutName[ran];
                    monday.Reps = 10;
                    monday.Sets = 3;
                    monday.Weight = 10;
                    monday.Met = 3;
                    monday.CaloriesBurned = Math.Round(CaloriesBurned(users, 3));
                    monday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Mondays.Add(monday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Moderate")
            {  
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    monday.Name = workoutName[ran];
                    monday.Reps = 8;
                    monday.Sets = 5;
                    monday.Weight = 25;
                    monday.Met = 6;
                    monday.CaloriesBurned = Math.Round(CaloriesBurned(users, 6));
                    monday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Mondays.Add(monday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Vigorous")
            {             
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    monday.Name = workoutName[ran];
                    monday.Reps = 5;
                    monday.Sets = 8;
                    monday.Weight = 45;
                    monday.Met = 9;
                    monday.CaloriesBurned = Math.Round(CaloriesBurned(users, 9));
                    monday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Mondays.Add(monday);
                    db.SaveChanges();
                }
            }
        }
     async public void TuesdayWorkout(User users, Workout workout)
        {
            Tuesday tuesday = new Tuesday();
            Random random = new Random();
            if (users.Category.ToString() == "Low")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                int temp = 0;
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);

                    tuesday.Name = workoutName[ran];
                    tuesday.Reps = 10;
                    tuesday.Sets = 3;
                    tuesday.Weight = 10;
                    tuesday.Met = 3;
                    tuesday.CaloriesBurned =Math.Round( CaloriesBurned(users, 3));
                    tuesday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Tuesdays.Add(tuesday);
                    db.SaveChanges();
                    ran = temp;
                }
            }
            if (users.Category.ToString() == "Moderate")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    tuesday.Name = workoutName[ran];
                    tuesday.Reps = 8;
                    tuesday.Sets = 5;
                    tuesday.Weight = 25;
                    tuesday.Met = 6;
                    tuesday.CaloriesBurned =Math.Round(CaloriesBurned(users, 6));
                    tuesday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Tuesdays.Add(tuesday);
                    db.SaveChanges();
                    ran = temp;
                }
            }
            if (users.Category.ToString() == "Vigorous")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    tuesday.Name = workoutName[ran];
                    tuesday.Reps = 5;
                    tuesday.Sets = 8;
                    tuesday.Weight = 45;
                    tuesday.Met = 9;
                    tuesday.CaloriesBurned = Math.Round(CaloriesBurned(users, 9));
                    tuesday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Tuesdays.Add(tuesday);
                    db.SaveChanges();
                    ran = temp;
                }
            }
        }
     async public void WedndayWorkout(User users, Workout workout)
        { 
            Wednesday wednesday = new Wednesday();
            Random random = new Random();
            if (users.Category.ToString() == "Low")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                int temp = 0;
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);

                    wednesday.Name = workoutName[ran];
                    wednesday.Reps = 10;
                    wednesday.Sets = 3;
                    wednesday.Weight = 10;
                    wednesday.Met = 3;
                    wednesday.CaloriesBurned = Math.Round(CaloriesBurned(users, 3));
                    wednesday.WorkoutID = workout.UserWorkoutID;
                    db.Wednesdays.Add(wednesday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Moderate")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    wednesday.Name = workoutName[ran];
                    wednesday.Reps = 8;
                    wednesday.Sets = 5;
                    wednesday.Weight = 25;
                    wednesday.Met = 6;
                    wednesday.CaloriesBurned = Math.Round(CaloriesBurned(users, 6));
                    wednesday.WorkoutID = workout.UserWorkoutID;
                    db.Wednesdays.Add(wednesday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Vigorous")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    wednesday.Name = workoutName[ran];
                    wednesday.Reps = 5;
                    wednesday.Sets = 8;
                    wednesday.Weight = 45;
                    wednesday.Met = 9;
                    wednesday.CaloriesBurned = Math.Round(CaloriesBurned(users, 9));
                    wednesday.WorkoutID = workout.UserWorkoutID;
                    db.Wednesdays.Add(wednesday);
                    db.SaveChanges();
                }
            }
        }
     async public void ThursdayWorkout(User users, Workout workout)
     {
            Thursday thursday = new Thursday();
            Random random = new Random();
            if (users.Category.ToString() == "Low")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                int temp = 0;
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);

                    thursday.Name = workoutName[ran];
                    thursday.Reps = 10;
                    thursday.Sets = 3;
                    thursday.Weight = 10;
                    thursday.Met = 3;
                    thursday.CaloriesBurned = Math.Round(CaloriesBurned(users, 3));
                    thursday.WorkoutID = workout.UserWorkoutID;
                    db.Thursdays.Add(thursday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Moderate")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    thursday.Name = workoutName[ran];
                    thursday.Reps = 8;
                    thursday.Sets = 5;
                    thursday.Weight = 25;
                    thursday.Met = 3;
                    thursday.CaloriesBurned = Math.Round(CaloriesBurned(users, 6));
                    thursday.WorkoutID = workout.UserWorkoutID;
                    db.Thursdays.Add(thursday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Vigorous")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    thursday.Name = workoutName[ran];
                    thursday.Reps = 5;
                    thursday.Sets = 8;
                    thursday.Weight = 45;
                    thursday.Met = 3;
                    thursday.CaloriesBurned = Math.Round(CaloriesBurned(users, 9));
                    thursday.WorkoutID = workout.UserWorkoutID;
                    db.Thursdays.Add(thursday);
                    db.SaveChanges();
                }
            }
        }
     async public void FridayWorkout(User users, Workout workout)
        {
            Friday friday = new Friday();
            Random random = new Random();
            if (users.Category.ToString() == "Low")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                int temp = 0;
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);

                    friday.Name = workoutName[ran];
                    friday.Reps = 10;
                    friday.Sets = 3;
                    friday.Weight = 10;
                    friday.Met = 3;
                    friday.CaloriesBurned = Math.Round(CaloriesBurned(users, 3));
                    friday.WorkoutID = workout.UserWorkoutID;
                    db.Fridays.Add(friday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Moderate")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    friday.Name = workoutName[ran];
                    friday.Reps = 8;
                    friday.Sets = 5;
                    friday.Weight = 25;
                    friday.Met = 6;
                    friday.CaloriesBurned = Math.Round(CaloriesBurned(users, 6));
                    friday.WorkoutID = workout.UserWorkoutID;
                    db.Fridays.Add(friday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Vigorous")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    friday.Name = workoutName[ran];
                    friday.Reps = 5;
                    friday.Sets = 8;
                    friday.Weight = 45;
                    friday.Met = 9;
                    friday.CaloriesBurned = Math.Round(CaloriesBurned(users, 9));
                    friday.WorkoutID = workout.UserWorkoutID;
                    db.Fridays.Add(friday);
                    db.SaveChanges();
                }
            }
        }
     async public void SaturdayWorkout(User users, Workout workout)
        {
            Saturday saturday = new Saturday();
            Random random = new Random();
            if (users.Category.ToString() == "Low")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                int temp = 0;
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);

                    saturday.Name = workoutName[ran];
                    saturday.Reps = 10;
                    saturday.Sets = 3;
                    saturday.Weight = 10;
                    saturday.Met = 3;
                    saturday.CaloriesBurned = Math.Round(CaloriesBurned(users, 3));
                    saturday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Saturdays.Add(saturday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Moderate")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    saturday.Name = workoutName[ran];
                    saturday.Reps = 8;
                    saturday.Sets = 5;
                    saturday.Weight = 25;
                    saturday.Met = 6;
                    saturday.CaloriesBurned = Math.Round(CaloriesBurned(users, 6));
                    saturday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Saturdays.Add(saturday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Vigorous")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    saturday.Name = workoutName[ran];
                    saturday.Reps = 5;
                    saturday.Sets = 8;
                    saturday.Weight = 45;
                    saturday.Met = 9;
                    saturday.CaloriesBurned = Math.Round(CaloriesBurned(users, 9));
                    saturday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Saturdays.Add(saturday);
                    db.SaveChanges();
                }
            }
        }
     async public void SundayWorkout(User users, Workout workout)
        {
            Sunday sunday = new Sunday();
            Random random = new Random();
            if (users.Category.ToString() == "Low")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                int temp = 0;
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                for (int i = 0; i < 4; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);

                    sunday.Name = workoutName[ran];
                    sunday.Reps = 10;
                    sunday.Sets = 3;
                    sunday.Weight = 10;
                    sunday.Met = 3;
                    sunday.CaloriesBurned = Math.Round(CaloriesBurned(users, 3));
                    sunday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Sundays.Add(sunday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Moderate")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    sunday.Name = workoutName[ran];
                    sunday.Reps = 8;
                    sunday.Sets = 5;
                    sunday.Weight = 25;
                    sunday.Met = 6;
                    sunday.CaloriesBurned = Math.Round(CaloriesBurned(users, 6));
                    sunday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Sundays.Add(sunday);
                    db.SaveChanges();
                }
            }
            if (users.Category.ToString() == "Vigorous")
            {
                var test = await GetWorkoutData(workout);
                List<string> workoutName = new List<string>();
                for (int i = 0; i < test.Count(); i++)
                {
                    var names = test[i]["name"].ToString();
                    if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What")
                    {
                        if (!workoutName.Contains(names))
                        {
                            workoutName.Add(names);
                        }
                    }
                }
                int temp = 0;
                for (int i = 0; i < 8; i++)
                {
                    int ran;
                    do
                    {
                        ran = random.Next(workoutName.Count());
                    } while (ran == temp);
                    sunday.Name = workoutName[ran];
                    sunday.Reps = 5;
                    sunday.Sets = 8;
                    sunday.Weight = 45;
                    sunday.Met = 9;
                    sunday.CaloriesBurned = Math.Round(CaloriesBurned(users, 9));
                    sunday.WorkoutID = workout.UserWorkoutID;
                    temp = ran;
                    db.Sundays.Add(sunday);
                    db.SaveChanges();
                }
            }
        }
    }
}
