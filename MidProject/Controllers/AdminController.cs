using MidProject.Auth;
using MidProject.DB;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MidProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        
        [AdminAccess]
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
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
                Role = "Admin"
            };
            var db = new Project_DBEntities();
            db.Users.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        
        public ActionResult AdminDash()
        {
            var db = new Project_DBEntities();
            var sales = (from p in db.Rents select p.Total_fear).Sum();
            var user = (from s in db.Users select s).Count();
            var car= (from s in db.Cars select s).Count();
            ViewBag.Carcount = car;
            ViewBag.Ucount = user;
            ViewBag.sum = sales;
            return View();
        }
        public ActionResult viewOrders()
        {
            var db = new Project_DBEntities();
            var Rent = db.Rents.ToList();

            return View(Rent);
        }
        public ActionResult PrintOrderList()
        {
            var report = new ActionAsPdf("pdfview");
            return report;
        }

        public ActionResult pdfview()
        {
            var db = new Project_DBEntities();
            var Rent = db.Rents.ToList();

            return View(Rent);
        }

    }
    }