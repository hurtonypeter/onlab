using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Perseus.Helpers
{
    public class StringHelper
    {
        private static string AllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#@$_*()";
        private static Random rng = new Random((int)DateTime.Now.Ticks);

        public static string RandomString(int minLength, int maxLength, string allowedChars = null)
        {
            if (allowedChars == null)
                allowedChars = AllowedChars;

            StringBuilder builder = new StringBuilder();
            char ch;
            int size = rng.Next(minLength, maxLength + 1);

            for(int i = 0; i < size; i++)
            {
                ch = allowedChars[rng.Next(allowedChars.Length)];
                builder.Append(ch);
            }

            return builder.ToString();
        }
    }
}