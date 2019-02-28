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
            var startdate = workout.StartDate.Date;
            var enddate = workout.EndDate.Date;
            WorkoutPerDay OneWeek = new WorkoutPerDay();
            OneWeek.Workout = workout;
            for (DateTime date = startdate; date <= enddate; date = date.AddDays(1))
            {
                if (date.DayOfWeek.ToString() == "Monday")
                {
                    if (OneWeek.Monday ==null)
                    {
                        var day1 = db.Mondays.Where(m => m.WorkoutID == workout.UserWorkoutID).Where(a => a.DayOfWeek == "Monday").Where(b => b.Date == date).ToList();
                        OneWeek.Monday = day1;
                       
                    }

                }
                if (date.DayOfWeek.ToString() == "Tuesday")
                {
                    if (OneWeek.Tuesday == null)
                    {
                        var day2 = db.Mondays.Where(m => m.WorkoutID == workout.UserWorkoutID).Where(a => a.DayOfWeek == "Tuesday").Where(b => b.Date == date).ToList();
                        OneWeek.Tuesday = day2;
                        
                    }

                }
                if (date.DayOfWeek.ToString() == "Wednesday")
                {
                    if (OneWeek.Wednesday == null)
                    {
                        var day3 = db.Mondays.Where(m => m.WorkoutID == workout.UserWorkoutID).Where(a => a.DayOfWeek == "Wednesday").Where(b => b.Date == date).ToList();
                        OneWeek.Wednesday = day3;
                        
                    }

                }
                if (date.DayOfWeek.ToString() == "Thursday")
                {
                    if (OneWeek.Thursday == null)
                    {
                        var day4 = db.Mondays.Where(m => m.WorkoutID == workout.UserWorkoutID).Where(a => a.DayOfWeek == "Thursday").Where(b => b.Date == date).ToList();
                        OneWeek.Thursday = day4;
                        
                    }

                }
                if (date.DayOfWeek.ToString() == "Friday")
                {
                    if (OneWeek.Friday == null)
                    {
                        var day5 = db.Mondays.Where(m => m.WorkoutID == workout.UserWorkoutID).Where(a => a.DayOfWeek == "Friday").Where(b => b.Date == date).ToList();
                        OneWeek.Friday = day5;
                       
                    }

                }
                if (date.DayOfWeek.ToString() == "Saturday")
                {
                    if (OneWeek.Saturday == null)
                    {
                        var day6 = db.Mondays.Where(m => m.WorkoutID == workout.UserWorkoutID).Where(a => a.DayOfWeek == "Saturday").Where(b => b.Date == date).ToList();
                        OneWeek.Saturday = day6;
                      
                    }

                }
                if (date.DayOfWeek.ToString() == "Sunday")
                {
                    if (OneWeek.Sunday == null)
                    {
                        var day7 = db.Mondays.Where(m => m.WorkoutID == workout.UserWorkoutID).Where(a => a.DayOfWeek == "Sunday").Where(b => b.Date == date).ToList();
                        OneWeek.Sunday = day7;
                        
                    }

                }

            }
            return View(OneWeek);
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
        public ActionResult UserInput(int id,string date)
        {

            var workout = db.Mondays.Where(w => w.WorkoutID == id).Where(d=>d.DayOfWeek == date).ToList();

              
            
            
            return View(workout);
        }

        // POST: Workout/Edit/5
        [HttpPost]
        public ActionResult UserInput(List<Monday> mondays)
        {
            var userLoggedin = User.Identity.GetUserId();
            var users = db.User.Where(u => u.ApplicationUserId == userLoggedin).FirstOrDefault();
            var type = mondays[0].WorkoutID;
            var edited = db.Mondays.Where(m => m.WorkoutID == type).ToList();
            try
            {
                for (int i = 0; i < mondays.Count; i++)
                {
                    edited[i].RepsCompleted = mondays[i].RepsCompleted;
                    edited[i].SetsCompleted = mondays[i].SetsCompleted;
                    var met = (int)edited[i].Met;
                    var time = (double)mondays[i].TimeCompleted;
                    edited[i].ActualCalories = CaloriesBurned(users, met, mondays.Count, time,1);
                    edited[i].TimeCompleted = time;
                }

                db.SaveChanges();
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View(edited);
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
        #region Weeks Calories Burned
        public ActionResult CaloriesBurnedForWeek(int? id)
        {
            var model = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            return View(model);
        }
        public ActionResult VisualizeCaloriesBurnedWeek(int? id)
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
        #endregion
        #region Monday Calories Burned
        public ActionResult CaloriesBurnedMonday(int? id)
        {
            var model = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            var monday = db.Mondays.Where(w => w.WorkoutID == model.UserWorkoutID).Where(d=>d.DayOfWeek == "Monday").ToList();
            return View(monday);
        }
        public ActionResult VisualizeCaloriesBurnedMonday(int? id)
        {
            var works = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            return Json(ListMonday(works.UserWorkoutID), JsonRequestBehavior.AllowGet);
        }
        public List<Monday> ListMonday(int? id)
        {
            List<Monday> mondays = new List<Monday>();
            mondays = db.Mondays.Where(m => m.WorkoutID == id).Where(d=>d.DayOfWeek == "Monday").ToList();
            return mondays;
        }
        #endregion
        #region Tuesday Calories Burned
        public ActionResult CaloriesBurnedTuesday(int? id)
        {
            var model = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            var monday = db.Mondays.Where(w => w.WorkoutID == model.UserWorkoutID).Where(d => d.DayOfWeek == "Tuesday").ToList();
            return View("CaloriesBurnedMonday", monday);
        }
        public ActionResult VisualizeCaloriesBurnedTuesday(int? id)
        {
            var works = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            return Json(ListTuesday(works.UserWorkoutID), JsonRequestBehavior.AllowGet);
        }
        public List<Monday> ListTuesday(int? id)
        {
            List<Monday> mondays = new List<Monday>();
            mondays = db.Mondays.Where(m => m.WorkoutID == id).Where(d => d.DayOfWeek == "Tuesday").ToList();
            return mondays;
        }
        #endregion
        #region Wednesday Calories Burned
        public ActionResult CaloriesBurnedWednesday(int? id)
        {
            var model = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            var monday = db.Mondays.Where(w => w.WorkoutID == model.UserWorkoutID).Where(d => d.DayOfWeek == "Wednesday").ToList();
            return View("CaloriesBurnedMonday", monday);
        }
        public ActionResult VisualizeCaloriesBurnedWednesday(int? id)
        {
            var works = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            return Json(ListWednesday(works.UserWorkoutID), JsonRequestBehavior.AllowGet);
        }
        public List<Monday> ListWednesday(int? id)
        {
            List<Monday> mondays = new List<Monday>();
            mondays = db.Mondays.Where(m => m.WorkoutID == id).Where(d => d.DayOfWeek == "Wednesday").ToList();
            return mondays;
        }
        #endregion
        #region Thursday Calories Burned
        public ActionResult CaloriesBurnedThursday(int? id)
        {
            var model = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            var monday = db.Mondays.Where(w => w.WorkoutID == model.UserWorkoutID).Where(d => d.DayOfWeek == "Thursday").ToList();
            return View("CaloriesBurnedMonday", monday);
        }
        public ActionResult VisualizeCaloriesBurnedThursday(int? id)
        {
            var works = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            return Json(ListThursday(works.UserWorkoutID), JsonRequestBehavior.AllowGet);
        }
        public List<Monday> ListThursday(int? id)
        {
            List<Monday> mondays = new List<Monday>();
            mondays = db.Mondays.Where(m => m.WorkoutID == id).Where(d => d.DayOfWeek == "Thursday").ToList();
            return mondays;
        }
        #endregion
        #region Friday Calories Burned
        public ActionResult CaloriesBurnedFriday(int? id)
        {
            var model = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            var monday = db.Mondays.Where(w => w.WorkoutID == model.UserWorkoutID).Where(d => d.DayOfWeek == "Friday").ToList();
            return View("CaloriesBurnedMonday", monday);
        }
        public ActionResult VisualizeCaloriesBurnedFriday(int? id)
        {
            var works = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            return Json(ListFriday(works.UserWorkoutID), JsonRequestBehavior.AllowGet);
        }
        public List<Monday> ListFriday(int? id)
        {
            List<Monday> mondays = new List<Monday>();
            mondays = db.Mondays.Where(m => m.WorkoutID == id).Where(d => d.DayOfWeek == "Friday").ToList();
            return mondays;
        }
        #endregion
        #region Saturday Calories Burned
        public ActionResult CaloriesBurnedSaturday(int? id)
        {
            var model = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            var monday = db.Mondays.Where(w => w.WorkoutID == model.UserWorkoutID).Where(d => d.DayOfWeek == "Saturday").ToList();
            return View("CaloriesBurnedMonday", monday);
        }
        public ActionResult VisualizeCaloriesBurnedSaturday(int? id)
        {
            var works = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            return Json(ListSaturday(works.UserWorkoutID), JsonRequestBehavior.AllowGet);
        }
        public List<Monday> ListSaturday(int? id)
        {
            List<Monday> mondays = new List<Monday>();
            mondays = db.Mondays.Where(m => m.WorkoutID == id).Where(d => d.DayOfWeek == "Saturday").ToList();
            return mondays;
        }
        #endregion
        #region Sunday Calories Burned
        public ActionResult CaloriesBurnedSunday(int? id)
        {
            var model = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            var monday = db.Mondays.Where(w => w.WorkoutID == model.UserWorkoutID).Where(d => d.DayOfWeek == "Sunday").ToList();
            return View("CaloriesBurnedMonday", monday);
        }
        public ActionResult VisualizeCaloriesBurnedSunday(int? id)
        {
            var works = db.Workouts.Where(w => w.UserWorkoutID == id).FirstOrDefault();
            return Json(ListSunday(works.UserWorkoutID), JsonRequestBehavior.AllowGet);
        }
        public List<Monday> ListSunday(int? id)
        {
            List<Monday> mondays = new List<Monday>();
            mondays = db.Mondays.Where(m => m.WorkoutID == id).Where(d => d.DayOfWeek == "Sunday").ToList();
            return mondays;
        }
        #endregion
        public double CaloriesBurned(User user,int mets,int workoutcount,double totaltime,double? passed)
        {
            double time=0;
            if (passed == null)
            {
                 time = (double)(totaltime / workoutcount) / 60;
            }
            else
            {
                time = totaltime/60;
            }
                    
        double energyExpenditure = time * (mets * (user.weight / 2.2f));
        return energyExpenditure;
    }
        //get list of workouts
        public async Task<List<JToken>> GetWorkoutData(Workout workout)
        {
            using (var httpClient = new HttpClient())
            {
                List<JToken> Holder = new List<JToken>();
                for (int i = 1; i < 30; i++)
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), $"https://wger.de/api/v2/exercise/?page={i}"))
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
                            if (lang == 2)
                            {
                                Holder.Add(json["results"][j]);
                            }

                        }
                    }
                }
                return Holder;
            }
        }
        //list of daliy workouts
     async public Task Mondayworkout(User users,Workout workout)
        {
            
            var test = await GetWorkoutData(workout);
            List<string> Day1 = new List<string>();
            List<string> Day2 = new List<string>();
            List<string> Day3 = new List<string>();
            List<string> Day4 = new List<string>();
            List<string> Day5 = new List<string>();
            List<string> Day6 = new List<string>();
            List<string> Day7 = new List<string>();

            Monday monday = new Monday();
            Random random = new Random();

            for (int i = 0; i < test.Count(); i++)
            {
                var type = test[i]["category"].ToObject<int>();
                var names = test[i]["name"].ToString();
                if (names != "" && names != "Abcd" && names != "Awesome" && names != "Test" && names != "What"&& names != "Aalex Gambe Alte al Muro")
                {
                    if (type == (int)workout.Category)
                    {
                        if (!Day1.Contains(names))
                        {
                            Day1.Add(names);
                        }
                    }
                    else if (type == (int)workout.Category2)
                    {
                        if (!Day2.Contains(names))
                        {
                            Day2.Add(names);
                        }
                    }
                    else if (type == (int)workout.Category3)
                    {
                        if (!Day3.Contains(names))
                        {
                            Day3.Add(names);
                        }
                    }
                    else if (type == (int)workout.Category4)
                    {
                        if (!Day4.Contains(names))
                        {
                            Day4.Add(names);
                        }
                    }
                    else if (type == (int)workout.Category5)
                    {
                        if (!Day5.Contains(names))
                        {
                            Day5.Add(names);
                        }
                    }
                    else if (type == (int)workout.Category6)
                    {
                        if (!Day6.Contains(names))
                        {
                            Day6.Add(names);
                        }
                    }
                    else if (type == (int)workout.Category7)
                    {
                        if (!Day7.Contains(names))
                        {
                            Day7.Add(names);
                        }
                    }
                }
            }
            List<List<string>> workoutdays = new List<List<string>>
            {
                Day1,
                Day2,
                Day3,
                Day4,
                Day5,
                Day6,
                Day7
            };
            DateTime sdateTime = new DateTime();
            DateTime edateTime = new DateTime();
            edateTime = workout.EndDate.Date;
            sdateTime = workout.StartDate.Date;
            if (users.Category.ToString() == "Low")
            {
                int itter = 0;
                for (DateTime dateTime = sdateTime; dateTime <= edateTime; dateTime = dateTime.AddDays(1))
                {
                    if (itter >6)
                    {
                        itter = 0;
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        #region Day1 parse
                        int ran;
                        ran = random.Next(workoutdays[itter].Count());
                        monday.Name = workoutdays[itter][ran];
                        monday.Reps = 10;
                        monday.Sets = 3;
                        monday.Weight = 10;
                        monday.Met = 3;
                        monday.Time = (45 / 8);
                        monday.DayOfWeek = dateTime.DayOfWeek.ToString();
                        monday.Date = dateTime.Date;
                        monday.CaloriesBurned = CaloriesBurned(users, 3, 8, 45, null);
                        monday.WorkoutID = workout.UserWorkoutID;
                        db.Mondays.Add(monday);
                        db.SaveChanges();
                        #endregion
                    }
                }
            }
            if (users.Category.ToString() == "Moderate")
            {
                int temp = 0;
                int itter = 0;
                for (DateTime dateTime = sdateTime; dateTime <= edateTime; dateTime = dateTime.AddDays(1))
                {
                    if (itter > 6)
                    {
                        itter = 0;
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        #region Day1
                        int ran;
                        do
                        {
                            ran = random.Next(workoutdays[itter].Count());
                        } while (ran == temp);
                        monday.Name = workoutdays[itter][ran];
                        monday.Reps = 8;
                        monday.Sets = 5;
                        monday.Weight = 25;
                        monday.Met = 6;
                        monday.Time = (90.0 / 8.0);
                        monday.Date = dateTime.Date;
                        monday.DayOfWeek = dateTime.DayOfWeek.ToString();
                        monday.CaloriesBurned = Math.Round(CaloriesBurned(users, 6, 8, 90, null));
                        monday.WorkoutID = workout.UserWorkoutID;
                        temp = ran;
                        db.Mondays.Add(monday);
                        db.SaveChanges();
                        #endregion
                    }

                    itter++;
                }
            }
            if (users.Category.ToString() == "Vigorous")
            {
                int temp = 0;
                int itter = 0;
                for (DateTime dateTime = sdateTime; dateTime <= edateTime; dateTime = dateTime.AddDays(1))
                {
                    if (itter > 6)
                    {
                        itter = 0;
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        #region Day1
                        int ran;
                        do
                        {
                            ran = random.Next(workoutdays[itter].Count());
                        } while (ran == temp);
                        monday.Name = workoutdays[itter][ran];
                        monday.Reps = 5;
                        monday.Sets = 8;
                        monday.Weight = 45;
                        monday.Met = 9;
                        monday.Time = (45 / 4);
                        monday.DayOfWeek = dateTime.DayOfWeek.ToString();
                        monday.Date = dateTime.Date;
                        monday.CaloriesBurned = Math.Round(CaloriesBurned(users, 9, 4, 45, null));
                        monday.WorkoutID = workout.UserWorkoutID;
                        temp = ran;
                        db.Mondays.Add(monday);
                        db.SaveChanges();
                        #endregion
                    }
                }
            }
        }
    }
}
