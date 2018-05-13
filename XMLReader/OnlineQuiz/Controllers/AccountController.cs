using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OnlineQuiz;
using System.Web.Security;
using SecurityLib;

namespace OnlineQuiz.Controllers
{
    public class AccountController : Controller
    {
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
                    USER user = db.USERs.FirstOrDefault((user) => user.UNAME.Equals(userIn.UNAME));

                    if(user != null)
                    {
                        Security.Hash(userIn.PASSW_HASH, user.SALT
                    }
                }
                    User user = (from u in db.Users
                                 where u.UserName.Equals(userIn.UserName)
                                 select u).FirstOrDefault<User>();
                if (user != null) // found a match
                    if (user.Password.Equals(userIn.Password))
                        //log into site:
                        FormsAuthentication.RedirectFromLoginPage(userIn.UserName, false);
            }
            //still here: either user not found, or password didn’t match
            ViewBag.ReturnUrl = ReturnUrl;
            ModelState.AddModelError("", "Invalid user name or password");
            return View();
        }
    }
}