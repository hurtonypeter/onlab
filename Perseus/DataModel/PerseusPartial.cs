using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Perseus.DataModel
{
    public partial class User
    {
        public string FullName
        {
            get { return this.LastName + " " + this.FirstName; }
        }
    }
}