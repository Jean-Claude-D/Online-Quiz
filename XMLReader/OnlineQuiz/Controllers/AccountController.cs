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
            return View(new USER());
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

                    if(user != null)
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
    }
}