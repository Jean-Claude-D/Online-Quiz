using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OnlineQuiz.Models;
using System.Web.Security;
using SecurityLib;

namespace OnlineQuiz.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            using (db1633477Entities db = new db1633477Entities())
            {
                var logged = db.USERs.First((user) => user.UNAME.Equals(System.Web.HttpContext.Current.User.Identity.Name));
                ViewBag.createdQuestions = logged.QUESTIONs.ToArray();
                ViewBag.answeredQuestions = logged.USER_ANSWER.ToArray();

                return View(logged);
            }
        }
        
        // GET: Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "UNAME, PASSW_HASH")] USER userIn, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                using (db1633477Entities db = new db1633477Entities())
                {
                    USER user = db.USERs.FirstOrDefault((currUser) => currUser.UNAME.Equals(userIn.UNAME));

                    if(user != default(USER))
                    {
                        string hashedPassword = Security.Hash(userIn.PASSW_HASH,
                            Convert.FromBase64String(user.SALT),
                            255);

                        if(hashedPassword.Equals(user.PASSW_HASH))
                        {
                            FormsAuthentication.RedirectFromLoginPage(userIn.UNAME, false);
                        }
                    }
                }
            }
            ViewBag.ReturnUrl = ReturnUrl;
            ModelState.AddModelError("", "Invalid user name or password");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register(string returnUrl)
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "UNAME, PASSW_HASH, FNAME, LNAME")] USER userIn, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                using (db1633477Entities db = new db1633477Entities())
                {
                    USER user = db.USERs.FirstOrDefault((currUser) => currUser.UNAME.Equals(userIn.UNAME));

                    if (user == default(USER))
                    {
                        string salt = Security.GetSalt(255);
                        userIn.SALT = salt;
                        userIn.PASSW_HASH = Security.Hash(userIn.PASSW_HASH, Convert.FromBase64String(salt), 255);
                        db.USERs.Add(userIn);
                        db.SaveChanges();
                        FormsAuthentication.RedirectFromLoginPage(userIn.UNAME, false);
                    }
                }
            }
            ViewBag.ReturnUrl = ReturnUrl;
            ModelState.AddModelError("", "UserName Already Taken");
            return View();
        }
    }
}