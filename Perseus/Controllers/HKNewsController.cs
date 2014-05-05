using Newtonsoft.Json;
using Perseus.API;
using Perseus.DataModel;
using Perseus.Filters;
using Perseus.Helpers;
using Perseus.Models;
using Perseus.Security;
using System;
using System.Collections.Generic;
using System.IO;
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

        //
        // GET: /HKNews/
        public ActionResult Index()
        {
            var papers = db.GetAllHKNewsPaper().ToList();

            return View(papers);
        }

        public ActionResult Edit(int id = 0)
        {
            if (id == 0)
            {
                HKNewsPaperViewModel model = new HKNewsPaperViewModel
                {
                    UserId = AccountHelper.CurrentUser().UserId,
                    RPublisher = SettingsApi.GetValue("hknews", "rpublisher"),
                    REditor = SettingsApi.GetValue("hknews", "reditor"),
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
            model.UserId = AccountHelper.CurrentUserId();
            if (model.IsDraft == false)
            {
                model.Sent = DateTime.Now;
            }
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

        public ActionResult Delete(int id)
        {
            db.DeleteHKNewsById(id);
            return RedirectToAction("Index", "HKNews");
        }

        public ActionResult Send(int id)
        {
            var paper = db.GetHKNewsPaperById(id);
            paper.Sent = DateTime.Now;
            paper.IsDraft = false;
            db.Save();
            MailHelper.SendHKNews(paper);

            return RedirectToAction("Index", "HKNews");
        }

        public ActionResult Settings()
        {
            var settings = SettingsApi.GetBag("hknews");

            FileStream file = new FileStream(Server.MapPath("~\\MailTemplates\\HKNews.cshtml"), FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            ViewBag.MailTemplate = sr.ReadToEnd();

            sr.Close();
            file.Close();

            return View(settings);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveTemplate(string template)
        {

            try
            {
                FileStream file = new FileStream(Server.MapPath("~\\MailTemplates\\HKNews.cshtml"), FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(file);

                sw.Write(template);

                sw.Close();
                file.Close();

                return this.Json(new { saved = true });
            }
            catch
            {
                return this.Json(new { saved = false });
            }
        }

        [HttpPost]
        public JsonResult SaveSetting(string setting)
        {

            try
            {
                Perseus.DataModel.Setting s = JsonConvert.DeserializeObject<Perseus.DataModel.Setting>(setting);
                SettingsApi.UpdateSetting(s.Bag, s.Key, s.Value, s.Description);

                return this.Json(new { saved = true });
            }
            catch
            {
                return this.Json(new { saved = false });
            }
        }
    }
}