using MidProject.DB;
using MidProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace MidProject.Controllers
{
    public class MailController : Controller
    {
        // GET: Mail
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FMail f)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

             
            var entities = new Project_DBEntities();
            var data = (from e in entities.Users
                        where e.Email.Equals(f.Email)
                        select e).FirstOrDefault();
            
            if (data == null)
            {
                TempData["Msg"] = "Invalid Credentials";
                return View();
            }
            var Oid = data.User_id;
            Random r = new Random();
            var x = r.Next(99999, 1000000).ToString();

            var m = "Your Forget Password OTP Is: " + x;
            var senderEmail = new MailAddress("shovon2186@gmail.com", "No_reply");
            var receiverEmail = new MailAddress(f.Email, "Receiver");
            var password = "sbfyatzqliswpvzx";
            var sub = "Recover Account";
            var body = m;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body
            })
            {
                smtp.Send(mess);
            }

            data.Otp = Convert.ToInt32(x);
            data.Otp_expired = DateTime.Now.AddMinutes(30);
            entities.SaveChanges();
  
            return RedirectToAction("OTP", "Mail",new {id=Oid});
            //return id with url
        }

        [HttpGet]
        public ActionResult OTP( int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult OTP(Otpsubmit c)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var db = new Project_DBEntities();
            var usr = (from p in db.Users where p.User_id == c.Id select p).SingleOrDefault();

            if(usr.Otp==c.Otp && usr.Otp_expired>=DateTime.Now)
            {
                usr.Password = c.Password;
                db.SaveChanges();
                TempData["Msg"] = "Password recover successfull";
                return RedirectToAction("Index","Home");
            }
            TempData["msg"] = "Invalid Credentials";
            return View();
        }
    }
}