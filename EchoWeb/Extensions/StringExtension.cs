using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EchoWeb.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Use the current thread's culture info for conversion
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
    }
}