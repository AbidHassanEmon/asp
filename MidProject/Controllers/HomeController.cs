using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MidProject.DB;
using MidProject.Models;

namespace MidProject.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (Session["Role"].Equals("Admin"))
                {
                    return RedirectToAction("AdminDash", "Admin");
                }
                else
                {
                    return RedirectToAction("UserDash", "User");
                }
            }   
                
            return View();
        }
        [HttpPost]
        public ActionResult Index(Login l)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            var entities = new Project_DBEntities();
            var data = (from e in entities.Users
                        where e.Password.Equals(l.Password) &&
                              e.User_name.Equals(l.Username)
                        select e).FirstOrDefault();
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie("data.User_name", false);
                Session["Role"] = data.Role;
                Session["User_id"] = data.User_id;
                Session["uname"] = data.User_name;
                if(data.Role=="Admin")
                {
                    return RedirectToAction("AdminDash","Admin");
                }
                else
                {
                    return RedirectToAction("UserDash","User");
                }
                
            }

            TempData["msg"] = "Invalid Credentials";
            return View();
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize]
        public ActionResult ViewProfile()
        {
            int id = Convert.ToInt32(Session["User_id"]);

            var db = new Project_DBEntities();
            var user = (from p in db.Users where p.User_id==id select p).SingleOrDefault();
            
            return View(user);
              
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult UpdateProfile()
        {
            int id = Convert.ToInt32(Session["User_id"]);
            var db = new Project_DBEntities();
            var user = (from p in db.Users where p.User_id == id select p).SingleOrDefault();

            return View(user);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateProfile(User c) //problem
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            int id = Convert.ToInt32(Session["User_id"]);
            var db = new Project_DBEntities();
            var pr = (from p in db.Users where p.User_id == id select p).SingleOrDefault();
            if(pr==null)
            {
                return View();
            }
            pr.Name = c.Name;
            //pr.Email = c.Email;
            pr.Address = c.Address;
            pr.Lisence_no = c.Lisence_no;

            db.SaveChanges();
            TempData["Msg"] = "Profile Updated Successfull";
            return RedirectToAction("ViewProfile", "Home"); 
        }
        [HttpGet]
        [Authorize]
        public ActionResult ChanePass()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChanePass(Chanepass c)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            int id = Convert.ToInt32(Session["User_id"]);
            var db = new Project_DBEntities();
            var pr = (from p in db.Users where p.User_id == id select p).SingleOrDefault();
            if (pr != null)
            {
                if (pr.Password == c.Password && c.Npassword==c.NNPassword)
                {
                    pr.Password = c.Npassword;
                    db.SaveChanges();
                    TempData["Msg"] = "Password Change Successfull";
                    return RedirectToAction("Logout", "Home");
                }
            }
            TempData["Msg"] = "Password Change Unuccessfull";
            return View();
        }

    }
}