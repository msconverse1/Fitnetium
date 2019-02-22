using Fitnetium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fitnetium.Controllers
{
    public class UserController : Controller
    {
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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
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
        //public float CaloriesBurned()
        //{
        //    var energyExpenditure = .0175 * MethodAccessException * (weight * 2.2f);
        //}
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
    }
}
