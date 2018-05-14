using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineQuiz.Models
{
    public class UserProfile
    {
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public QUESTION[] AnsweredQuestions { get; set; }
        public QUESTION[] CreatedQuestions { get; set; }

        public UserProfile() { }

        public UserProfile(
            string Username,
            string Firstname,
            string Lastname,
            QUESTION[] AnsweredQuestions,
            QUESTION[] CreatedQuestions)
        {
            this.Username = Username;
            this.Firstname = Firstname;
            this.Lastname = Lastname;
            this.AnsweredQuestions = AnsweredQuestions;
            this.CreatedQuestions = CreatedQuestions;
        }
    }
}