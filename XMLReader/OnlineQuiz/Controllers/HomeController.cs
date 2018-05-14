using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OnlineQuiz.Models;
using System.Web.Security;

namespace OnlineQuiz.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            bool userAuth = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            string uname = System.Web.HttpContext.Current.User.Identity.Name;

            CategoryModel[] categories = null;
            if (ModelState.IsValid)
            {
                using (db1633477Entities db = new db1633477Entities())
                {
                    var cats = db.CATEGORies.Where((x) => x.QUESTIONs.Any()).OrderBy((x) => x.QUESTIONs.Count());
                    categories = new CategoryModel[cats.Count()];

                    int i = 0;
                    int numQs;

                    foreach (var cat in cats)
                    {
                        if(userAuth)
                        {
                            numQs = db
                                .QUESTIONs
                                .Count((question) => question.CATEGORY_ID == cat.ID &&
                                    !db.USERs
                                    .FirstOrDefault((user) => user.UNAME.Equals(uname))
                                    .USER_ANSWER
                                    .Any((user_answer) => user_answer.QUES_ID.Equals(question.ID)));
                        }
                        else
                        {
                            numQs = db
                                .QUESTIONs
                                .Count((question) => question.CATEGORY_ID == cat.ID);
                        }

                        categories[i] = new CategoryModel(cat, numQs);
                        i++;
                    }
                }

            }

            return View(categories);
        }

        [Authorize]
        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCategory([Bind(Include = "TITLE, DESCRIPTION")] CATEGORY newCategory, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                using (db1633477Entities db = new db1633477Entities())
                {
                    CATEGORY withSameTitle = db.CATEGORies.FirstOrDefault((category) => category.TITLE.Equals(newCategory.TITLE));

                    if(withSameTitle == default(CATEGORY))
                    {
                        db.CATEGORies.Add(newCategory);
                        db.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
            }

            ModelState.AddModelError("", "Category Name already taken");
            return View();
        }
    }
}