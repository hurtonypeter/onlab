using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Perseus.DataModel;
using Perseus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Perseus.Security
{
    public class AccountHelper
    {
        //public const string SUPERUSER = "admin";

        public static User CurrentUser()
        {
            if (IsAuthenticated())
            {
                using (Entities db = new Entities())
                {
                    var uid = HttpContext.Current.User.Identity.GetUserId();
                    var user = db.User.SingleOrDefault(u => u.UserId.Equals(uid));

                    //ha authenticated(van sütije), de ez mégis null-t ad vissza, akkor vmi turpisság van a dologban!!
                    if (user == null)
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
                    // ha bejelentkezett felhasnzálóval van dolgunk, kérdezzük le a jogait
                    return db.GetCurrentPermissions(HttpContext.Current.User.Identity.GetUserId()).ToList();
                }
                else
                {
                    // ha nincs bejelentkezve, akkor az anonymous jogokat kérdezzük le
                    return db.Role.SingleOrDefault(r => r.Name.ToLower().Equals("anonymous")).Permission.Select(p => p.Name).ToList();
                }
                
            }
        }

        public static bool HasPermission(string perm)
        {
            // TODO: superusert beépíteni
            //if (IsAuthenticated() && CurrentUser().UserName == SUPERUSER)
            //    return true;

            return CurrentPermissions().Contains(perm, StringComparer.OrdinalIgnoreCase);
        }
    }
}