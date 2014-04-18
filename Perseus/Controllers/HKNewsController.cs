using Newtonsoft.Json;
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

        public ActionResult New()
        {
            
            XmlReader reader = XmlReader.Create("http://vik.hk/rss.xml");
            SyndicationFeed Feed = SyndicationFeed.Load(reader);
            
            List<RSSItem> rss = new List<RSSItem>();
            foreach (var item in Feed.Items)
            {
                rss.Add(new RSSItem {
                    Link = item.BaseUri +  item.Id,
                    Title = item.Title.Text,
                    Body = item.Summary.Text
                });
            }

            ViewBag.RSS = new MvcHtmlString(JsonConvert.SerializeObject(rss));

            HKNewsPaperViewModel model = new HKNewsPaperViewModel
            {
                //UserId = AccountHelper.CurrentUser().UserId,
                RPublisher = "Bakos Asztrik, a Hallgatói Képviselet elnöke",
                REditor = "Görbe Richárd, PR felelős"
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
            string asd = JsonConvert.SerializeObject(model);
            ViewBag.Mvcstring = new MvcHtmlString(asd);
            ViewBag.Json = asd;

            return View(model);
        }
	}
}