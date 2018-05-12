using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Xml;
using System.Xml.Linq;
using SecurityLib;

namespace XMLReader
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Testing Security */

            string password = "MyPassword";
            Console.WriteLine("Hashing : " + password);
            byte[] salt = Security.GetBytesSalt(255);
            Console.WriteLine("Salt : " + Convert.ToBase64String(salt));
            Console.WriteLine("Hashed : " + Security.Hash(password, salt, 255));

            ///* Testing argument list */
            //if(args == null || args.Length <= 0)
            //{
            //    throw new ArgumentException
            //        ("Invalid Argument:" + Environment.NewLine +
            //        "[NO ARGUMENT]" + Environment.NewLine +
            //        "Usage:" + Environment.NewLine +
            //        System.AppDomain.CurrentDomain.FriendlyName +
            //        " xmlFile0 [xmlFile1 xmlFile2 ... xmlFileN]"
            //        + Environment.NewLine);
            //}

            //foreach(string xmlFile in args)
            //{
            //    /* Testing File */
            //    if(!File.Exists(xmlFile))
            //    {
            //        throw new ArgumentException
            //            ("Invalid Argument:" + Environment.NewLine +
            //            xmlFile + Environment.NewLine +
            //            "Could not find file" + Environment.NewLine +
            //            "Please verify the file name is typed correctly"
            //            + Environment.NewLine);
            //    }

            //    try
            //    {
            //        XDocument xmlDoc = XDocument.Load(xmlFile);

            //        string rootElement = xmlDoc.Root.Name.ToString();
            //        switch (rootElement)
            //        {
            //            case "questions":
            //                Console.WriteLine("Parsing Questions");
            //                using (db1633477Entities db = new db1633477Entities())
            //                {
            //                    QuestionParserXML.parse(db, xmlDoc);
            //                }
            //                break;
            //            case "users":
            //                Console.WriteLine("Not Yet Parsing Users");
            //                //Parsing users
            //                break;
            //            default:
            //                Console.Error.WriteLine
            //                    ("Invalid Argument:" + Environment.NewLine +
            //                    rootElement + Environment.NewLine +
            //                    "Has not associated parsing Class" + Environment.NewLine +
            //                    "Ignoring" + Environment.NewLine);
            //                break;
            //        }
            //    }
            //    /* If the file is not a valid xml Document */
            //    catch(XmlException)
            //    {
            //        throw new ArgumentException
            //            ("Invalid Argument:" + Environment.NewLine +
            //            xmlFile + Environment.NewLine +
            //            "Could not read file" + Environment.NewLine +
            //            "Please verify the file is a well-formed and valid xml file"
            //            + Environment.NewLine);
            //    }
            //}
        }
    }
}
