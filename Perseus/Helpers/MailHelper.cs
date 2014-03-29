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

        public static void SendPassword()
        {
            dynamic email = new Email("~/MailTemplates/Teszt.cshtml");
            email.To = "hurtonypeter@gmail.com";
            email.Send();
        }

    }
}