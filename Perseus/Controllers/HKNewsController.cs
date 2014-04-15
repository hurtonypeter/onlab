using Perseus.Models;
using Perseus.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Perseus.Controllers
{
    public class HKNewsController : Controller
    {
        //
        // GET: /HKNews/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            HKNewsPaperViewModel model = new HKNewsPaperViewModel
            {
                User = AccountHelper.CurrentUser()
            };

            return View(model);
        }
	}
}