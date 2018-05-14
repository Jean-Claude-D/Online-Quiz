using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.Runtime.Serialization;

namespace SecurityLib
{
    public class EncryptionManager
    {
        private static UnicodeEncoding _encoding = new UnicodeEncoding();
        private static EncryptionManager _instance;
        public static EncryptionManager getInstance()
        {
            if(_instance == null)
            {
                _instance = new EncryptionManager();
            }

            return _instance;
        }

        Dictionary<int, RSACryptoServiceProvider> keys;

        private EncryptionManager()
        {
            keys = new Dictionary<int, RSACryptoServiceProvider>();
        }

        public string GetPublicKey(int delay, out int id)
        {
            int newId = 0;
            while (keys.Any((entry) => entry.Key == newId++)) ;

            /* Generate a new entry in the dictionary */
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            keys.Add(newId, rsa);

            /* If the user never decrypts the message, clean */
            removeRSA(newId, delay);

            Console.WriteLine("Hello there");

            id = newId;
            /* return RSA, but not include private component */
            return rsa.ToXmlString(false);
        }

        private async void removeRSA(int id, int delay)
        {
            await Task.Delay(delay);
            keys.Remove(id);
        }

        public static string Encrypt(string key, string message)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(key);

            byte[] toEncrypt = _encoding.GetBytes(message);

            return _encoding.GetString(rsa.Encrypt(toEncrypt, false));
        }

        public string Decrypt(int id, string message)
        {
            if (keys.TryGetValue(id, out RSACryptoServiceProvider rsaFound))
            {
                byte[] decrypted = rsaFound.Decrypt(_encoding.GetBytes(message), false);
                keys.Remove(id);

                return _encoding.GetString(decrypted);
            }
            else
            {
                throw new KeyTimedOutException();
            }
        }
    }

    public class KeyTimedOutException : Exception
    {
        public KeyTimedOutException() : this("The key you are searching for has timed out, try again")
        {
        }

        public KeyTimedOutException(string message) : base(message)
        {
        }

        public KeyTimedOutException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
