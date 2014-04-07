using Perseus.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perseus.Controllers
{
    public class MenuController : Controller
    {
        protected Entities db = new Entities();
        //
        // GET: /Menu/
        public ActionResult Index()
        {
            var menu = db.MenuItem.ToList();

            return View();
        }
	}
}