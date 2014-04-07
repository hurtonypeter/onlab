using Perseus.DataModel;
using Perseus.Models;
using Perseus.Security;
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
        public PartialViewResult MainMenu()
        {
            var mainmenu = db.Menu.SingleOrDefault(m => m.Name.Equals("main-menu"))
                .MenuItem.Where(s => s.Parent == null).ToList();

            List<MenuViewModel> model = new List<MenuViewModel>();

            foreach (var item in mainmenu)
            {
                MenuViewModel menu = new MenuViewModel
                {
                    Path = item.LinkPath,
                    Title = item.LinkTitle
                };
                if(item.Children != null)
                {
                    foreach (var child in item.Children)
                    {
                        if(AccountHelper.HasPermission(child.Permission.Name))
                        {
                            menu.Children.Add(new MenuViewModel
                            {
                                Path = child.LinkPath,
                                Title = child.LinkTitle
                            });
                        }
                    }
                }
                model.Add(menu);
            }

            return PartialView("~/Views/Shared/_Menu.cshtml", model);
        }

	}
}