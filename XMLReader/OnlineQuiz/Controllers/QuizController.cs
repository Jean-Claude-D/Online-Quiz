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
            string user = System.Web.HttpContext.Current.User.Identity.Name;
            QuizModel model = new QuizModel();
            using (db1633477Entities db = new db1633477Entities())
            {
                QUESTION[] unansweredQuestions = db.QUESTIONs
                    .Where((question) => question.CATEGORY_ID == categoryId)
                    .Where((question) =>
                        !db.USER_ANSWER
                        .Any((user_answer) => user_answer.UNAME.Equals(user) && user_answer.QUES_ID.Equals(question.ID)))
                    .Take(5).ToArray();

                RandomizedQuestion[] randomOrder = new RandomizedQuestion[unansweredQuestions.Length];
                for(int i = 0; i < randomOrder.Length; i++)
                {
                    randomOrder[i] = new RandomizedQuestion(unansweredQuestions[i]);
                }

                model = new QuizModel(randomOrder);
            }

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(QuestionResult[] userAnswers)
        {
            using (db1633477Entities db = new db1633477Entities())
            {
                foreach (QuestionResult answer in userAnswers)
                {
                    bool correctAnswer = db.QUESTIONs
                        .FirstOrDefault((question) => question.ID.Equals(answer.Id))
                        .OPT_A.Equals(answer.Choice);

                    db.USER_ANSWER.Add(new USER_ANSWER()
                    {
                        UNAME = System.Web.HttpContext.Current.User.Identity.Name,
                        CORR_ANS = correctAnswer,
                        QUES_ID = answer.Id
                    });
                    db.SaveChanges();
                }
            }
            return RedirectToRoute("Home/Index");
        }
    }
}