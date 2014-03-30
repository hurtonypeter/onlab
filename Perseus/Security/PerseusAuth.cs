using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perseus.Security
{
    public class PerseusAuth : AuthorizeAttribute
    {
        public string Action { get; set; }

        public PerseusAuth() : base()
        {
            this.Action = null;
        }

        public PerseusAuth(string action) : base()
        {
            this.Action = action;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // ha allowanonymoust adtak meg elvárt jogként, akkor engedjük át
            if (Action.ToLower().Equals("allowanonymous"))
                return true;

            if (AccountHelper.HasPermission(Action))
                return true;
            else
                return false;
        }
    }
}