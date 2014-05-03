using Perseus.DataModel;
using Postal;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Perseus.Helpers
{
    public class MailHelper
    {

        public static void WelcomeSendPassword(string username, string toaddress, string pass)
        {
            dynamic email = new Email("~/MailTemplates/Teszt.cshtml");
            email.To = toaddress;
            email.UserName = username;
            email.Password = pass;
            email.Send();
        }
        public static bool SendHKNews(HKNewsPaper paper)
        {
            dynamic email = new Email("~/MailTemplates/HKNews.cshtml");
            email.Model = paper;
            email.Send();

            return true;
        }
    }
}