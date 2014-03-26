using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Perseus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Perseus.Security
{
    public class AccountHelper
    {
        /*public const string SUPERUSER = "admin";

        public static User CurrentUser()
        {
            if (IsAuthenticated())
            {
                using (Entities db = new Entities())
                {
                    var uid = IdentityExtensions.GetUserId(HttpContext.Current.User.Identity);
                    var user = db.User.SingleOrDefault(u => u.UserId.Equals(uid));

                    //ha authenticated(van sütije), de ez mégis null-t ad vissza, akkor vmi turpisság van a dologban!!
                    if(user == null)
                    {
                        throw new UnauthorizedAccessException();
                    }

                    return user;
                }
            }
            else
            {
                return null;
            }
        }

        public static bool IsAuthenticated()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        private static List<string> CurrentPermissions()
        {
            using (Entities db = new Entities())
            {
                if (IsAuthenticated())
                {
                    return new List<string>();
                }
                else
                {
                    return new List<string>();
                }

            }
        }

        public static bool HasPermission(string perm)
        {
            if (IsAuthenticated() && CurrentUser().UserName == SUPERUSER)
                return true;

            List<string> current = CurrentPermissions();
            return current.Contains(perm);
        }*/
    }
}