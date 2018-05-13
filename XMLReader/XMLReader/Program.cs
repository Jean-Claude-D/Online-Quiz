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

            EncryptionManager em = EncryptionManager.getInstance();

            /* a minute of delay */
            string publicKey = em.GetPublicKey(60_000, out int id);

            string message = "Hello World";
            Console.WriteLine("Plain message : [" + message + ']');
            string encrypted = EncryptionManager.Encrypt(publicKey, message);
            Console.WriteLine("Encrypted : [" + encrypted + ']');

            string decrypted = EncryptionManager.getInstance().Decrypt(id, encrypted);
            Console.WriteLine("Decrypted : [" + decrypted + ']');

            //string password = "MyPassword";
            //Console.WriteLine("Hashing : " + password);
            //byte[] saltBytes;
            //string salt = Security.GetSalt(255, out saltBytes);
            //Console.WriteLine("Salt : " + salt);
            //Console.WriteLine("Hashed : " + Security.Hash(password, saltBytes, 255));

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
