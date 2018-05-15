using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineQuiz.Models
{
    public class RandomizedQuestion
    {
        private static Random _rand;
        static RandomizedQuestion()
        {
            _rand = new Random();
        }

        public QUESTION InnerQuestion
        {
            get;
            set;
        }

        public int GetInnerId()
        {
            return InnerQuestion.ID;
        }

        public IEnumerable<string> GetChoices()
        {
            foreach(int randomIndex in _randomList(4))
            {
                switch (randomIndex)
                {
                    case 0:
                        yield return InnerQuestion.OPT_A;
                        break;
                    case 1:
                        yield return InnerQuestion.OPT_B;
                        break;
                    case 2:
                        yield return InnerQuestion.OPT_C;
                        break;
                    case 3:
                        yield return InnerQuestion.OPT_D;
                        break;
                }
            }
        }

        private IEnumerable<int> _randomList(int length)
        {
            LinkedList<int> availables = new LinkedList<int>();
            for (int i = 0; i < length - 1; availables.AddLast(i++)) ;

            foreach(int rand in availables)
            {
                yield return availables.ElementAt(_rand.Next(availables.Count));
            }
        }

        public RandomizedQuestion(QUESTION innerQuestion)
        {
            this.InnerQuestion = innerQuestion;
        }
    }
}