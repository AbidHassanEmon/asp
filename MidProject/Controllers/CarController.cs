using MidProject.Auth;
using MidProject.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MidProject.Controllers
{
    [AdminAccess]
    public class CarController : Controller
    {
        // GET: Car
        public ActionResult Index()
        {
            var db = new Project_DBEntities();
            var cars = db.Cars.ToList();

            return View(cars);   
        }


        [HttpGet]
        public ActionResult AddCar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCar(Car c)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var p = new Car() { Name = c.Name, Model = c.Model, Reg_year = c.Reg_year,
                Mileage=c.Mileage,Rent=c.Rent,Description=c.Description};

            var db = new Project_DBEntities();
            db.Cars.Add(p);
            db.SaveChanges();
            TempData["Msg"] = "Car Added Successfull";
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var db = new Project_DBEntities();
            var user = (from p in db.Cars where p.Car_id == id select p).SingleOrDefault();
            db.Cars.Remove(user);
            db.SaveChanges();
            TempData["Msg"] = "Cars deleted Successfull";
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new Project_DBEntities();
            var car = (from p in db.Cars where p.Car_id == id select p).SingleOrDefault();
            return View(car);
        }
        [HttpPost]
        public ActionResult Edit(int id, Car c)
        {
            var db = new Project_DBEntities();
            var car = (from p in db.Cars where p.Car_id == id select p).SingleOrDefault();
            car.Name = c.Name;
            car.Model = c.Model;
            car.Reg_year = c.Reg_year;
            car.Mileage = c.Mileage;
            car.Rent = c.Rent;
            car.Description = c.Description;
            db.SaveChanges();
            TempData["Msg"] = "Cars Edited Successfull";
            return RedirectToAction("Index");
        }

    }
}