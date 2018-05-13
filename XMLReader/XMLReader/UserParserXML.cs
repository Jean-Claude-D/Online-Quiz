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
            string passwHash;

            string fname;
            string lname;

            foreach (XElement userXML in xmlDocument.Root.Elements())
            {
                password = XMLParserHelper.GetStringAttribute(userXML, "password") ?? "password";
                Console.WriteLine("Getting Salt");
                salt = Security.GetSalt(255);

                uname = XMLParserHelper.GetStringAttribute(userXML, "userId");
                passwHash = Security.Hash(password, Convert.FromBase64String(salt), 255);
                fname = XMLParserHelper.GetStringAttribute(userXML, "firstName");
                lname = XMLParserHelper.GetStringAttribute(userXML, "lastName");

                var alreadyExistingUser = db
                    .USERs
                    .FirstOrDefault((user) => user.UNAME.Equals(uname));
                if (default(USER) != alreadyExistingUser)
                {
                    Console.WriteLine("Updating...\t" + uname);

                    alreadyExistingUser.PASSW_HASH = passwHash;
                    alreadyExistingUser.SALT = salt;
                    alreadyExistingUser.FNAME = fname;
                    alreadyExistingUser.LNAME = lname;
                }
                else
                {
                    Console.WriteLine("Adding...\t" + uname);

                    db.USERs.Add(new USER
                    {
                        UNAME = uname,
                        PASSW_HASH = passwHash,
                        SALT = salt,
                        FNAME = fname,
                        LNAME = lname
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
