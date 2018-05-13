using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineQuiz.Models
{
    public struct LoginCredentials
    {
        public string Username
        {
            get;
            private set;
        }
        public string Password
        {
            get;
            private set;
        }
    }
}