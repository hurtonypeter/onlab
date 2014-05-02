using Newtonsoft.Json;
using Perseus.DataModel;
using Perseus.Filters;
using Perseus.Models;
using Perseus.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;

namespace Perseus.Controllers
{
    public class HKNewsController : Controller
    {
        PerseusRepository db = new PerseusRepository();

        class RSSItem
        {
            public string Title { get; set; }
            public string Link { get; set; }
            public string Body { get; set; }
        }
        //
        // GET: /HKNews/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New(int id = 0)
        {
            if(id == 0)
            {
                HKNewsPaperViewModel model = new HKNewsPaperViewModel
                {
                    UserId = AccountHelper.CurrentUser().UserId,
                    RPublisher = "Bakos Asztrik, a Hallgatói Képviselet elnöke",
                    REditor = "Görbe Richárd, PR felelős",
                    UserName = AccountHelper.CurrentUser().FullName,
                    IsNew = true
                };
                model.NewsItems.Add(new HKNewsItemViewModel
                {
                    Title = "első hír",
                    Link = "http://proba.hu",
                    Body = "szöveg törgye blablabla blabla"
                });
                model.NewsItems.Add(new HKNewsItemViewModel
                {
                    Title = "második hír",
                    Link = "http://proba2.hu/megmeg",
                    Body = "szöveg törgye blablabla blabla"
                });

                ViewBag.model = new MvcHtmlString(JsonConvert.SerializeObject(model));
            }
            else
            {
                var paper = db.GetHKNewsPaperById(id);
                ViewBag.model = new MvcHtmlString(JsonConvert.SerializeObject(HKNewsPaperViewModel.ViewModelFromPaper(paper)));
            }

            return View();
        }

        [HttpPost]
        public ActionResult Save([FromJson] HKNewsPaperViewModel model)
        {
            if (model.IsNew)
            {
                db.AddHKNewsPaper(model.PaperFromViewModel());
            }
            else
            {
                db.UpdateHKNewsPaper(model.PaperFromViewModel());
            }

            return RedirectToAction("Index", "HKNews");
        }
	}
}