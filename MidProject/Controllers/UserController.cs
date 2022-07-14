using MidProject.Auth;
using MidProject.DB;
using MidProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MidProject.Controllers
{
    [UserAccess]
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(User c)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var p = new User()
            {
                Name = c.Name,
                Email = c.Email,
                Dob = c.Dob,
                Address = c.Address,
                Lisence_no = c.Lisence_no,
                User_name = c.User_name,
                Password = c.Password,
                Role = "User"
            };
            var db = new Project_DBEntities();
            db.Users.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult UserDash()
        {
            var db = new Project_DBEntities();
            var cars = db.Cars.ToList();

            return View(cars);

        }

        [HttpGet]
        public ActionResult OrderP(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [HttpPost]
        public ActionResult OrderP(Rent r)
        {
            var db = new Project_DBEntities();
            var Rent = (from p in db.Cars where p.Car_id == r.Car_id select p.Rent).SingleOrDefault();

            var date = r.Pickup_time;
            var rDate = r.Return_time;
            var s = Convert.ToDateTime(date);
            var Re = Convert.ToDateTime(rDate);
            var Day = Convert.ToInt32(Re.Subtract(s).TotalDays);
            int Total = Rent * Day;
            var R = new Rent()
            {
                Car_id = r.Car_id,
                User_id = r.User_id,
                Pickup_time = date,
                Return_time = rDate,
                Total_fear = Total

            };

            db.Rents.Add(R);

            db.SaveChanges();

            TempData["Msg"] = "Rents Successfull";
            return RedirectToAction("UserDash", "User");
        }

        [HttpGet]
        public ActionResult RentAcar(Search c)
        {
            if (c.Pdate == null || c.Rdate == null)
            {
                return View();
            }

            var db = new Project_DBEntities();

            //update in db and edmx then apply less or gater logic and good to go
            //var rent =from b in db.Rents where b.Car_id==104 select b;

            var rent = new List<int>();
            var test = db.Rents.ToList();
            foreach (var b in test)
            {
                if ((c.Pdate >= Convert.ToDateTime(b.Pickup_time)) && (c.Pdate <= Convert.ToDateTime(b.Return_time)) ||
                (c.Rdate >= Convert.ToDateTime(b.Pickup_time)) && (c.Rdate <= Convert.ToDateTime(b.Return_time)) ||
                (c.Pdate <= Convert.ToDateTime(b.Pickup_time)) && (c.Rdate >= Convert.ToDateTime(b.Pickup_time)) && (c.Rdate <= Convert.ToDateTime(b.Return_time)) ||
                (c.Pdate >= Convert.ToDateTime(b.Pickup_time)) && (c.Pdate <= Convert.ToDateTime(b.Return_time)) && (c.Rdate >= Convert.ToDateTime(b.Return_time)) ||
                (c.Pdate <= Convert.ToDateTime(b.Pickup_time)) && (c.Rdate >= Convert.ToDateTime(b.Return_time)))
                {

                    rent.Add(b.Car_id);
                }
            }

            //var rent = test.Where(b => (c.Pdate >= Convert.ToDateTime(b.Pickup_time)) && (c.Pdate <= Convert.ToDateTime(b.Return_time)) ||
            //    (c.Rdate >= Convert.ToDateTime(b.Pickup_time)) && (c.Rdate <= Convert.ToDateTime(b.Return_time)) ||
            //    (c.Pdate <= Convert.ToDateTime(b.Pickup_time)) && (c.Rdate >= Convert.ToDateTime(b.Pickup_time)) && (c.Rdate <= Convert.ToDateTime(b.Return_time)) ||
            //    (c.Pdate >= Convert.ToDateTime(b.Pickup_time)) && (c.Pdate <= Convert.ToDateTime(b.Return_time)) && (c.Rdate >= Convert.ToDateTime(b.Return_time)) ||
            //    (c.Pdate <= Convert.ToDateTime(b.Pickup_time)) && (c.Rdate >= Convert.ToDateTime(b.Return_time))).ToList();

            var cars = db.Cars.Where(r => !rent.Contains(r.Car_id));
            foreach (var item in cars)
            {
                c.Car.Add(item);
            }
            return View(c);

        }

    }
}