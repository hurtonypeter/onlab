using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Perseus.Models;
using Perseus.DataModel;
using Perseus.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Perseus.Helpers;
using Postal;

namespace Perseus.Controllers
{
    public class PeopleController : Controller
    {
        private PerseusRepository db = new PerseusRepository();

        public ActionResult Index()
        {
            var model = db.GetAllUser().OrderBy(s => s.UserName).ToList();

            return View(model);
        }

        public ActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(NewUserModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUserManager um = new ApplicationUserManager(new ApplicationUserStore(new ApplicationDbContext()));
                var pass = StringHelper.RandomString(8, 10);
                var user = new ApplicationUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    Email = model.Email,
                    Created = DateTime.Now,
                    LastLogin = null
                };
                var result = um.Create(user, pass);
                if(result.Succeeded)
                {
                    MailHelper.WelcomeSendPassword(user.UserName, user.Email, pass);
                    return RedirectToAction("Index", "People");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(model);
        }

        public ActionResult EditUser(string id)
        {
            User user = db.GetUserById(id);
            EditAccountModel model = Mapper.Map<User, EditAccountModel>(user);

            List<Role> allrole = db.GetAllRole().ToList();
            List<Role> userroles = user.Role.ToList();
            foreach (var item in allrole)
            {
                model.Roles.Add(new RoleCheckBox(item.Name, userroles.Contains(item)));
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(EditAccountModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.EditUser(model);
                    return RedirectToAction("Index", "People");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.ToString());
                }
            }
            return View(model);
        }
        public ActionResult DeleteUser(string id)
        {
            db.DeleteUserById(id);

            return RedirectToAction("Index", "People");
        }

        public ActionResult Roles()
        {
            var model = db.GetAllRole().ToList();
            
            return View(model);
        }

        public ActionResult AddRole()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRole(Role model)
        {
            try
            {
                db.CreateNewRole(model);
                return RedirectToAction("Roles", "People");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.ToString());
            }

            return View(model);
        }

        public ActionResult EditRole(string id)
        {
            Role model = db.GetRoleById(id);

            //az anonymous "role" nem szerkeszthető, törölhető
            if (model.Name == "anonymous")
                return RedirectToAction("Roles", "People");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(Role model)
        {
            //az anonymous "role" nem szerkeszthető, törölhető
            if (model.Name == "anonymous")
                return RedirectToAction("Roles", "People");

            try
            {
                db.EditRole(model);
                return RedirectToAction("Roles", "People");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.ToString());
            }

            return View(model);
        }

        public ActionResult DeleteRole(string id)
        {
            db.DeleteRoleById(id);

            return RedirectToAction("Roles", "People");
        }

        public ActionResult Permissions()
        {
            PermissionsTableModel model = new PermissionsTableModel();

            //TODO: mappinget átmozgatni a MappingHelperbe
            model.Roles = db.GetAllRole().ToList();
            List<Permission> permissions = db.GetAllPermission().ToList();
            List<Module> modules = db.GetModules().ToList();

            int m = 0;
            for (int i = 0; i < modules.Count; i++)
            {
                model.Modules.Add(new ModuleRoles());
                model.Modules[i].ModuleName = modules[i].ModuleName;
                foreach (var item in permissions)
                {
                    if (item.Module.ModuleName == modules[i].ModuleName)
                    {
                        model.Modules[i].AccessLevels.Add(new ModuleRolesLine(item.PermissionId, item.Name));
                        for (int j = 0; j < model.Roles.Count; j++)
                        {
                            if (item.Role.Contains(model.Roles[j]))
                            {
                                model.Modules[i].AccessLevels[m].Boxes.Add(true);
                            }
                            else
                            {
                                model.Modules[i].AccessLevels[m].Boxes.Add(false);
                            }
                        }

                        m++;
                    }
                }
                m = 0;
            }
            
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Permissions(PermissionsTableModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: a mappinget átmozgatni a MappingHelperbe
                    List<Role> roles = db.GetAllRole().ToList();
                    List<Permission> permissions = db.GetAllPermission().ToList();
                    List<Module> modules = db.GetModules().ToList();

                    for (int i = 0; i < model.Modules.Count; i++)
                    {
                        for (int j = 0; j < model.Modules[i].AccessLevels.Count; j++)
                        {
                            for (int k = 0; k < model.Modules[i].AccessLevels[j].Boxes.Count; k++)
                            {
                                if (model.Modules[i].AccessLevels[j].Boxes[k])
                                {
                                    if (permissions.SingleOrDefault(p => p.PermissionId == model.Modules[i].AccessLevels[j].Pid).Role.Contains(roles.SingleOrDefault(r => r.Id.Equals(model.Roles[k].Id))) == false)
                                    {
                                        db.AddRoleToPermission(model.Roles[k].Id, model.Modules[i].AccessLevels[j].Pid);
                                    }
                                }
                                else
                                {
                                    if (permissions.SingleOrDefault(p => p.PermissionId == model.Modules[i].AccessLevels[j].Pid).Role.Contains(roles.SingleOrDefault(r => r.Id.Equals(model.Roles[k].Id))) == true)
                                    {
                                        db.DeleteRoleFromPermission(model.Roles[k].Id, model.Modules[i].AccessLevels[j].Pid);
                                    }
                                }
                            }
                        }
                    }

                    return RedirectToAction("Permissions", "People");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.ToString());
                }
            }

            return View(model);
        }
	}
}