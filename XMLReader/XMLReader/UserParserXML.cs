using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.Xml.Linq;

using SecurityLib;

namespace XMLReader
{
    public static class UserParserXML
    {
        public static void Parse(db1633477Entities db, XDocument xmlDocument)
        {
            string uname;
            string password;
            string salt;
            byte[] saltBytes;

            foreach (XElement userXML in xmlDocument.Root.Elements())
            {
                password = XMLParserHelper.GetStringAttribute(userXML, "password") ?? "password";
                salt = Security.GetSalt(255, out saltBytes);

                uname = XMLParserHelper.GetStringAttribute(userXML, "userId");

                if(db.USERs.Any((user) => user.UNAME.Equals(uname)))
                {
                    Console.WriteLine("Already Exists...\t" + uname);
                }
                else
                {
                    Console.WriteLine("Adding...\t" + uname);
                    db.USERs.Add(new USER
                    {
                        UNAME = uname,
                        PASSW_HASH = Security.Hash(password, saltBytes, 255),
                        SALT = salt,
                        FNAME = XMLParserHelper.GetStringAttribute(userXML, "firstName"),
                        LNAME = XMLParserHelper.GetStringAttribute(userXML, "lastName")
                    });
                }
            }

            Console.WriteLine("Saving Changes...");
            try
            {
                db.SaveChanges();
            }
            catch(DbUpdateException exc)
            {
                Console.WriteLine(exc);
                throw exc;

            }
            catch(DbEntityValidationException exc)
            {
                foreach(DbEntityValidationResult error in exc.EntityValidationErrors)
                {
                    Console.WriteLine(error.ToString() + " :\n" +
                        error.Entry.Entity.ToString() + " is" + (error.IsValid ? " " : " not ") + "valid\n");
                    foreach(DbValidationError problem in error.ValidationErrors)
                    {
                        Console.WriteLine(problem.ErrorMessage);
                    }
                }
                throw exc;
            }
        }
    }
}
