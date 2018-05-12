using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

namespace XMLReader
{
    public static class QuestionParserXML
    {
        public static void Parse(db1633477Entities db, XDocument xmlDocument)
        {
            foreach(XElement xmlQuestion in xmlDocument.Root.Elements())
            {
                var categoryID = parseCategory(db, XMLParserHelper.GetStringChild(xmlQuestion, "category"));
                var questionTitle = XMLParserHelper.GetStringChild(xmlQuestion, "title");

                /* If question already exists */
                if(db.QUESTIONs.Any((q) => q.CATEGORY_ID == categoryID && q.TITLE.Equals(questionTitle)))
                {
                    Console.WriteLine("Already Exists...\t" + questionTitle);
                }
                else
                {
                    Console.WriteLine("Parsing...\t" + questionTitle);
                    var options = orderOptions(
                        xmlQuestion.Elements().Where((child) => child.Name.LocalName.Equals("option")));

                    try
                    {
                        db.QUESTIONs.Add(new QUESTION
                        {
                            TITLE = questionTitle,
                            CATEGORY_ID = categoryID,
                            AUTHOR = XMLParserHelper.GetStringChild(xmlQuestion, "user"),
                            OPT_A = options[0],
                            OPT_B = options[1],
                            OPT_C = options[2],
                            OPT_D = options[3]
                        });
                    }
                    catch(DbUpdateException)
                    {
                        Console.WriteLine("Ignore...\t" + questionTitle);
                    }
                }
            }

            Console.WriteLine("Saving Changes...");
            db.SaveChanges();
        }

        private static int parseCategory(db1633477Entities db, string categoryTitle)
        {
            bool duplicate = db.CATEGORies
                .Any((category) => category.TITLE.Equals(categoryTitle));

            if(!duplicate)
            {
                Console.WriteLine("Adding...\t" + categoryTitle);
                db.CATEGORies.Add(new CATEGORY()
                {
                    TITLE = categoryTitle
                });
                db.SaveChanges();
            }

            return db.CATEGORies
                .Single((category) => category.TITLE.Equals(categoryTitle))
                .ID;
        }

        private static string[] orderOptions(IEnumerable<XElement> options)
        {
            var correctOption = options
                .FirstOrDefault((option) => {
                    var correctAttr = option
                        .Attributes()
                        .FirstOrDefault((attr) => attr.Name.LocalName.Equals("correct"));

                    return correctAttr?.Value?.Equals("true") ?? false;
                    });
            var otherOptions = options
                .Where((option) => !option.Equals(correctOption));

            string[] optionsStr = new string[otherOptions.Count() + (correctOption == null ? 0 : 1)];
            optionsStr[0] = correctOption.Value;
            for(int i = 1; i < optionsStr.Length; i++)
            {
                optionsStr[i] = otherOptions.ElementAt(i - 1).Value;
            }

            return optionsStr;
        }
    }
}
