using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineQuiz.Models
{
    public class CategoryModel
    {
        public CATEGORY Category { get; set; }
        public int NumQuestions { get; set; }
        public CategoryModel(CATEGORY Category, int NumQuestions)
        {
            this.Category = Category;
            this.NumQuestions = NumQuestions;
        }
    }
}