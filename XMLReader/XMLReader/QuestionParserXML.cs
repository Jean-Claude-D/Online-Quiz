using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

namespace XMLReader
{
    public static class QuestionParserXML
    {
        public static IList<QUESTION> parse(XDocument xmlDocument)
        {
            LinkedList<QUESTION> questions = new LinkedList<QUESTION>();

            foreach(XElement xmlQuestion in xmlDocument.Root.Elements())
            {
                Console.WriteLine(xmlQuestion.Name.ToString());
            }

            return null;
        }
    }
}
