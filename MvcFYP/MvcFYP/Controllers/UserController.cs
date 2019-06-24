using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFYP.Models;
using System.Net.Mail;
using System.Net;

namespace MvcFYP.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult AddOrEdit(int id = 0)
        {
            User userModel = new User();
            return View(userModel);
        }

        [HttpPost]
        public ActionResult AddOrEdit(User userModel)
        {

            using (FYPEntities db = new FYPEntities())
            {
                if (db.Users.Any(x => x.Username == userModel.Username))
                {
                    ViewBag.DuplicationMessage = "User already exist!";
                    return View("AddOrEdit", userModel);
                }

                db.Users.Add(userModel);
                db.SaveChanges();
            }

            string fromaddr = "2019UtarFyp@gmail.com";
            string toaddr = userModel.Email;//TO ADDRESS HERE
            string password = "admin12345abc";


            MailMessage msg = new MailMessage();
            msg.Subject = "Account Created";
            msg.From = new MailAddress(fromaddr);
            msg.Body = "Username: " + userModel.Username + "\nPassword: " + userModel.Password;
            msg.To.Add(new MailAddress(userModel.Email));
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential(fromaddr, password);
            smtp.Credentials = nc;
            smtp.Send(msg);

            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful!";
            return View("AddOrEdit", new User());
        }

        public ActionResult Index()
        {
            FYPEntities db = new FYPEntities();

            return View(db.Users.ToList());
        }

        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            FYPEntities db = new FYPEntities();

            var userDetails = db.Users.Find(id);
            db.Users.Remove(userDetails);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}