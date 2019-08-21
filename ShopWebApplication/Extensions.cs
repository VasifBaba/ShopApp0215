using ShopWebApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ShopWebApplication
{
    public static class Extensions
    {
        public static bool CheckInput(string[] arr, string emp)
        {
            foreach (var list in arr)
            {
                if (list == emp)
                {
                    return false;
                }
            }
            return true;
        }

        public static string hashMe(this string password)
        {
            byte[] byteArr = new ASCIIEncoding().GetBytes(password);
            byte[] hashArr = new SHA256Managed().ComputeHash(byteArr);
            string hashedPassword = new ASCIIEncoding().GetString(hashArr);
            return hashedPassword;
        }

    }
}

