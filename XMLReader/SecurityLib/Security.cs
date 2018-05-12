using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SecurityLib
{
    /* Lots of this is from StackOverflow and .Net docs */
    public static class Security
    {
        private static RNGCryptoServiceProvider _rand;
        private static Random _unsecureRand;

        static Security()
        {
            _rand = new RNGCryptoServiceProvider();
            _unsecureRand = new Random();
        }

        public static string GeneratePassword()
        {
            string file = "worstPasswordList.txt";
            try
            {
                using (StreamReader stream = new StreamReader(file))
                {
                    string[] passwords = stream.ReadToEnd().Split('\n');

                    return passwords[_unsecureRand.Next(passwords.Length)];
                }
            }
            catch (IOException)
            {
                throw new Exception("Error Accessing : " + file);
            }

        }

        public static string Hash(string password, byte[] salt, int hashLength)
        {
            var hashedPassword = new Rfc2898DeriveBytes(password, salt, 10000)
                .GetBytes(toBase64BitCount(hashLength) * 8);

            return Convert.ToBase64String(hashedPassword);
        }

        public static string GetSalt(int length)
        {
            return Convert.ToBase64String(GetBytesSalt(toBase64BitCount(length)));
        }

        public static byte[] GetBytesSalt(int length)
        {
            byte[] salt = new byte[length];
            _rand.GetBytes(salt);

            return salt;
        }

        private static int toBase64BitCount(int bytesCount)
        {
            /* Each set of Base64 is formed of 6 bits */
            return (int)Math.Ceiling((double)bytesCount / 6);
        }
    }
}
