using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using OnlineQuiz.Models;

namespace OnlineQuiz.Controllers
{
    public class QuizController : Controller
    {
        // GET: Quiz
        [Authorize]
        public ActionResult Index(int? categoryId)
        {
            return View();
        }
    }
}