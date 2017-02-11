using FileCloud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FileCloud.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
       
        public ActionResult Index(UserModel usermodel)
        {
            if (!ModelState.IsValid)
                return View(usermodel);
            using (var db = new FileCloudDatabaseEntities())
            {
                if (db.User.Any(model => model.username.Contains(usermodel.username))) return Redirect(HttpContext.Request.UrlReferrer.AbsoluteUri);
                var user = new User();
                var salt = SecurityService.HashClass.GenerateRandomSalt(12);
                user.username = usermodel.username;
                user.passHash = SecurityService.HashClass.GenerateSaltedHash(usermodel.password, salt);
                user.salt = salt;
                user.email = usermodel.email;
                db.User.Add(user);
                db.SaveChanges();
                ModelState.Clear();
                Console.WriteLine("Register Successfull..");
                ViewBag.Message = "Successfully Registration Done...";
            }
            return View(usermodel);
        }

    }
}