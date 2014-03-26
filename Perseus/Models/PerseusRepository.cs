using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Perseus.Models
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

        public void EditUser(EditAccountModel model)
        {
            /*User user = GetUserById(model.UId);
            user.UserName = model.UserName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.Status = model.Status;

            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            if (model.Password != null)
            {
                UserManager.PasswordHasher.HashPassword(model.Password);
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

            Save();*/
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
            RoleManager<IdentityRole> rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            rm.Create(new IdentityRole(model.Name));
            //db.Role.Add(model);
            //Save();
        }


        public IQueryable<Permission> GetAllPermission()
        {
            return db.Permission;
        }
        public IQueryable<Module> GetModules()
        {
            return db.Module;
        }
    }
}