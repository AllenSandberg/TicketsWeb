using System;
using System.Security.Cryptography;
using System.Text;

namespace BuisnessLogic.Utils
{
    public class CryptoUtils
    {
        private static string _keyPassword;

        public static string KeyPassword { set { _keyPassword = value; } }
        internal static string KeyPasswordProtected { get { return _keyPassword; } }
        public static string HashData(string text)
        {
            var data = Encoding.UTF8.GetBytes(text);
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(data);

            return Convert.ToBase64String(sha1data).ToUpper();
        }

        public static string HashDataSHA512(string text)
        {
            var data = Encoding.UTF8.GetBytes(text);
            byte[] hash;
            using (SHA512 shaM = new SHA512Managed())
            {
                hash = shaM.ComputeHash(data);
            }
            return Convert.ToBase64String(hash).ToUpper();
        }

    }
}
