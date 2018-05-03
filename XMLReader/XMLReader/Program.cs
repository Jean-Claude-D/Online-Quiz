using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Xml;
using System.Xml.Linq;

namespace XMLReader
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Testing argument list */
            if(args == null || args.Length <= 0)
            {
                throw new ArgumentException
                    ("Invalid Argument:" + Environment.NewLine +
                    "[NO ARGUMENT]" + Environment.NewLine +
                    "Usage:" + Environment.NewLine +
                    System.AppDomain.CurrentDomain.FriendlyName +
                    " xmlFile0 [xmlFile1 xmlFile2 ... xmlFileN]"
                    + Environment.NewLine);
            }

            foreach(string xmlFile in args)
            {
                /* Testing File */
                if(!File.Exists(xmlFile))
                {
                    throw new ArgumentException
                        ("Invalid Argument:" + Environment.NewLine +
                        xmlFile + Environment.NewLine +
                        "Could not find file" + Environment.NewLine +
                        "Please verify the file name is typed correctly"
                        + Environment.NewLine);
                }

                try
                {
                    XDocument xmlDoc = XDocument.Load(xmlFile);

                    string rootElement = xmlDoc.Root.Name.ToString();
                    switch (rootElement)
                    {
                        case "questions":
                            //Parsing questions
                            break;
                        case "users":
                            //Parsing users
                            break;
                        default:
                            Console.Error.WriteLine
                                ("Invalid Argument:" + Environment.NewLine +
                                rootElement + Environment.NewLine +
                                "Has not associated parsing Class" + Environment.NewLine +
                                "Ignoring" + Environment.NewLine);
                            break;
                    }
                }
                /* If the file is not a valid xml Document */
                catch(XmlException)
                {
                    throw new ArgumentException
                        ("Invalid Argument:" + Environment.NewLine +
                        xmlFile + Environment.NewLine +
                        "Could not read file" + Environment.NewLine +
                        "Please verify the file is a well-formed and valid xml file"
                        + Environment.NewLine);
                }
            }
        }
    }
}
