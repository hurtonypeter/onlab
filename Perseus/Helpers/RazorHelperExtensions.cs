using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perseus.Helpers
{
    public static class RazorHelperExtensions
    {
        // Prepend the provided path with the scheme, host, and port of the request.
        public static string FormatAbsoluteUrl(this Uri url, string path)
        {
            return string.Format(
               "{0}/{1}", url.FormatUrlStart(), path.TrimStart('/'));
        }

        // Generate a string with the scheme, host, and port if not 80.
        public static string FormatUrlStart(this Uri url)
        {
            return string.Format("{0}://{1}{2}", url.Scheme,
               url.Host, url.Port == 80 ? string.Empty : ":" + url.Port);
        }
        public static string LinkFromRelative(this HtmlHelper helper, string relativePath)
        {
            if (relativePath.StartsWith("/"))
            {
                relativePath = relativePath.TrimStart(new Char[] { '/' });
            }
            var url = HttpContext.Current.Request.Url;

            return url.Scheme + "://" + url.Authority + "/" + relativePath;
        }
    }
}