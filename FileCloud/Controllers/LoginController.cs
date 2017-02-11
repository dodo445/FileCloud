using FileCloud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FileCloud.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {   
            if (Session["LogedUser"] != null) { return RedirectToAction("Index", "UserPage");}
            return View();
        }

        [HttpPost]
        
        public ActionResult Index(UserModel usermodel)
        {   
            using (var db = new FileCloudDatabaseEntities())
            {    
                var user = db.User.FirstOrDefault(u => u.username == usermodel.username);
                if (user != null &&
                    user.passHash == SecurityService.HashClass.GenerateSaltedHash(usermodel.password, user.salt))
                {                   
                    Session["LogedUser"] = user.username;
                    Session["LogedUserID"] = user.id;
                    return RedirectToAction("Index", "UserPage");
                }
            }
            return View(usermodel);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}