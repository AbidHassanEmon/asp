using MidProject.Auth;
using MidProject.DB;
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
                Dob=c.Dob,
                Address=c.Address,
                Lisence_no=c.Lisence_no,
                User_name=c.User_name,
                Password=c.Password,
                Role="User"
            };
            var db = new Project_DBEntities();
            db.Users.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
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
            var Re=Convert.ToDateTime(rDate);
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
    }
}