using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineQuiz.Models
{
    public class QuizModel
    {
        public QUESTION[] questions;

        public QuizModel()
        {

        }

        public QuizModel(QUESTION[] questions)
        {
            this.questions = questions;
        }
    }
}