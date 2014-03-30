using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Perseus.DataModel
{
    public class Repository : IDisposable
    {
        protected Entities db = new Entities();

        public void Save()
        {
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();

        }
    }
}