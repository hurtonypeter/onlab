using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using Perseus.Helpers;
using Perseus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;

namespace Perseus.DataModel
{
    public class PerseusRepository : IDisposable
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
        
        public IQueryable<User> GetAllUser()
        {
            return db.User;
        }

        public User GetUserById(string id)
        {
            return db.User.SingleOrDefault(s => s.UserId.Equals(id));
        }

        //public void CreateNewUser(NewUserModel model)
        //{
        //    ApplicationUserManager um = new ApplicationUserManager(new ApplicationUserStore(new ApplicationDbContext()));
        //    var user = new ApplicationUser()
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        UserName = model.UserName,
        //        //PasswordHash = um.PasswordHasher.HashPassword(StringHelper.RandomString(8, 10)),
        //        Email = model.Email,
        //        Created = DateTime.Now,
        //        LastLogin = null
        //    };
        //    um.Create(user, um.PasswordHasher.HashPassword(StringHelper.RandomString(8, 10)));
        //}

        public void EditUser(EditAccountModel model)
        {
            User user = GetUserById(model.UId);
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Status = model.Status;

            ApplicationUserManager UserManager = new ApplicationUserManager(new ApplicationUserStore(new ApplicationDbContext()));

            if (model.Password != null)
            {
                user.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                
            }

            List<string> removeIndex = new List<string>();
            for (int i = 0; i < user.Role.Count; i++)
            {
                if (!(model.Roles.Contains(new RoleCheckBox(user.Role.ElementAt(i).Name, false))))
                {
                    removeIndex.Add(user.Role.ElementAt(i).Id);
                }
            }
            foreach (var index in removeIndex)
            {
                Role role = GetRoleById(index);
                user.Role.Remove(role);
            }

            for (int i = 0; i < model.Roles.Count; i++)
            {
                if (model.Roles[i].Checked)
                {
                    try
                    {
                        Role role = GetRoleByName(model.Roles[i].Text);
                        user.Role.Add(role);
                    }
                    catch { }
                }
            }

            Save();
        }

        public void DeleteUserById(string id)
        {
            db.User.Remove(GetUserById(id));
            Save();
        }

        public IQueryable<Role> GetAllRole()
        {
            return db.Role;
        }
        public Role GetRoleById(string id)
        {
            return db.Role.SingleOrDefault(s => s.Id.Equals(id));
        }
        public Role GetRoleByName(string name)
        {
            return db.Role.SingleOrDefault(s => s.Name == name);
        }
        public void CreateNewRole(Role model)
        {
            
            ApplicationRoleManager rm = new ApplicationRoleManager(new ApplicationRoleStore(new ApplicationDbContext()));
            rm.Create(new ApplicationRole(model.Name)
            { 
                Id = Guid.NewGuid().ToString()
            });
        }
        public void EditRole(Role model)
        {
            Role role = GetRoleById(model.Id);
            role.Name = model.Name;
            Save();
        }

        public void DeleteRoleById(string id)
        {
            Role role = GetRoleById(id);
            // ha az anonymous rolet próbálnák törölni, ne hagyjuk:)
            if (role.Name.Equals("anonymous") || role == null)
            {
                return;
            }
            db.Role.Remove(role);
            Save();
        }

        public IQueryable<Permission> GetAllPermission()
        {
            return db.Permission;
        }
        public IQueryable<Module> GetModules()
        {
            return db.Module;
        }
        public void AddRoleToPermission(string rid, int pid)
        {
            Role role = GetRoleById(rid);
            db.Permission.SingleOrDefault(p => p.PermissionId == pid).Role.Add(role);
            Save();
        }
        public void DeleteRoleFromPermission(string rid, int pid)
        {
            Role role = GetRoleById(rid);
            db.Permission.SingleOrDefault(p => p.PermissionId == pid).Role.Remove(role);
            Save();
        }
    }
}