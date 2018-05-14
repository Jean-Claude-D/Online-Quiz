using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineQuiz.Models
{
    public class CatergoryModel
    {
        public String CatName { get; set; }
        public int NumQuestions { get; set; }
        public CatergoryModel(String CatNames, int NumQuestions)
        {
            this.CatName = CatName;
            this.NumQuestions = NumQuestions;
        }
    }
}