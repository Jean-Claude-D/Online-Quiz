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
            if (ModelState.IsValid)
            {
                using (db1633477Entities db = new db1633477Entities())
                {
                    var cats = db.CATEGORies.Where((x) => x.QUESTIONs.Any()).OrderBy((x) => x.QUESTIONs.Count());
                    CatergoryModel[] catsArray = new CatergoryModel[cats.Count()];
                    int i = 0;
                    foreach (var cat in cats)
                    {
                        String name = cat.TITLE;
                        int numQs = db.QUESTIONs.Count((x) => x.CATEGORY_ID == cat.ID);
                        catsArray[i] = new CatergoryModel(name, numQs);
                        i++;
                    }
                    ViewBag.categories = catsArray;
                }
            }
            return View();
        }

        //UNAUTHENTICATED
        //display categories sorted by number of questions
        //Ask to login to start quiz or add a question

        //1. get list of categories

        //AUTHENTICATED
        //display categories sorted by number of questions not answered
        //link to start quiz
        //link to add new categories => check if category exists already
    }
}