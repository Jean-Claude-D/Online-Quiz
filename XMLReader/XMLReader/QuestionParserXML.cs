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
                var question = new
                {
                    user = XMLParserHelper.getStringChild(xmlQuestion, "user"),
                    category = XMLParserHelper.getStringChild(xmlQuestion, "category"),
                    title = XMLParserHelper.getStringChild(xmlQuestion, "title"),
                    opts = xmlQuestion.Elements().Select((child) => child.Name.LocalName.Equals("option"))
                };
                Console.WriteLine("Parsing...\t" + question.title);


            }

            return null;
        }

        /*private static int parseCategory()
        {
            return 0;
        }*/
    }
}
