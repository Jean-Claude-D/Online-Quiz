using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineQuiz.Models
{
    public class QuizModel
    {
        public RandomizedQuestion[] questions;

        public QuizModel()
        {
            this.questions = new RandomizedQuestion[0];
        }

        public QuizModel(RandomizedQuestion[] questions)
        {
            this.questions = questions;
        }
    }
}