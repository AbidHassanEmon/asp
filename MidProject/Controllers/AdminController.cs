using MidProject.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MidProject.Controllers
{
    public class AdminController : Controller
    {
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
        [Authorize]
        public ActionResult AdminDash()
        {
            return View();
        }
    }
}