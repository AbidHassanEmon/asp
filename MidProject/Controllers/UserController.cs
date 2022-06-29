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

        public ActionResult UserDash()
        {
            return View();
        }
    }
}