using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FileCloud.SecurityService
{
    public class HashClass
    {
        public static string GenerateSaltedHash(string password, string _salt)
        { // http://stackoverflow.com/questions/2138429/hash-and-salt-passwords-in-c-sharp
            password = password.Trim();
            _salt = _salt.Trim();
            byte[] plainText = StrToByte(password);
            byte[] salt = StrToByte(_salt);
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return Convert.ToBase64String(algorithm.ComputeHash(plainTextWithSaltBytes));
        }

        public static byte[] StrToByte(string text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static string ByteToStr(byte[] bytetext)

        {
            return Convert.ToBase64String(bytetext);
        }

        public static string GenerateRandomSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            var bytes = new Byte[size];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}