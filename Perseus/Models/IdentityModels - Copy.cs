using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;

namespace Perseus.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastLogin { get; set; }
        public ICollection<ApplicationUserRole> Roles { get; set; }

    }
    public class ApplicationUserRole : IdentityUserRole
    {

    }

    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationRolePermisson> Permissions { get; set; }
    }
    public class ApplicationRolePermisson
    {
        public ApplicationRole Role { get; set; }
        public string RoleId { get; set; }
        public ApplicationPermission Permission { get; set; }
        public int PermissionId { get; set; }
    }
    public class ApplicationPermission
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public ICollection<ApplicationRolePermisson> Roles { get; set; }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public virtual IDbSet<ApplicationPermission> Permission { get; set; }
        
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            /*modelBuilder.Entity<IdentityUser>().ToTable("Users");
            EntityTypeConfiguration<ApplicationUser> table =
                modelBuilder.Entity<ApplicationUser>().ToTable("Users");

            // EF won't let us swap out IdentityUserRole for ApplicationUserRole here:
            modelBuilder.Entity<ApplicationUser>().HasMany<ApplicationUserRole>((ApplicationUser u) => u.Roles);
            modelBuilder.Entity<ApplicationUserRole>().HasKey((ApplicationUserRole r) =>
                new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("UserRoles");

            // Leave this alone:
            EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
                modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
                    new { UserId = l.UserId, LoginProvider = l.LoginProvider, ProviderKey = l.ProviderKey }).ToTable("UserLogins");

            
            // Add this, so that IdentityRole can share a table with ApplicationRole:
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");


            // Change these from IdentityRole to ApplicationRole:
            EntityTypeConfiguration<ApplicationRole> entityTypeConfiguration1 = modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            entityTypeConfiguration1.Property((ApplicationRole r) => r.Name).IsRequired();


            modelBuilder.Entity<ApplicationRole>().HasMany<ApplicationRolePermisson>((ApplicationRole r) => r.Permissions);

            modelBuilder.Entity<ApplicationRolePermisson>().HasKey((ApplicationRolePermisson r) =>
                new { PermissionId = r.PermissionId, RoleId = r.RoleId }).ToTable("RolePermission");



            modelBuilder.Entity<ApplicationPermission>().ToTable("Permission");*/

            // Keep this:
            modelBuilder.Entity<IdentityUser>().ToTable("Users");

            // Change TUser to ApplicationUser everywhere else - IdentityUser and ApplicationUser essentially 'share' the AspNetUsers Table in the database:
            EntityTypeConfiguration<ApplicationUser> table =
                modelBuilder.Entity<ApplicationUser>().ToTable("Users");

            table.Property((ApplicationUser u) => u.UserName).IsRequired();

            // EF won't let us swap out IdentityUserRole for ApplicationUserRole here:
            modelBuilder.Entity<ApplicationUser>().HasMany<ApplicationUserRole>((ApplicationUser u) => u.Roles);
            modelBuilder.Entity<ApplicationUserRole>().HasKey((ApplicationUserRole r) =>
                new { UserId = r.UserId, RoleId = r.RoleId }).ToTable("UserRoles");


            // Add the group stuff here:
            /*modelBuilder.Entity<ApplicationUser>().HasMany<ApplicationUserGroup>((ApplicationUser u) => u.Groups);
            modelBuilder.Entity<ApplicationUserGroup>().HasKey((ApplicationUserGroup r) => new { UserId = r.UserId, GroupId = r.GroupId }).ToTable("ApplicationUserGroups");*/

            // And here:
            modelBuilder.Entity<ApplicationPermission>().HasMany<ApplicationRolePermisson>((ApplicationPermission g) => g.Roles);
            modelBuilder.Entity<ApplicationRolePermisson>().HasKey((ApplicationRolePermisson gr) => new { RoleId = gr.RoleId, PermissionId = gr.PermissionId }).ToTable("RolePermission");

            // And Here:
            EntityTypeConfiguration<ApplicationPermission> groupsConfig = modelBuilder.Entity<ApplicationPermission>().ToTable("Permissions");
            groupsConfig.Property((ApplicationPermission r) => r.Name).IsRequired();

            // Leave this alone:
            EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
                modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
                    new { UserId = l.UserId, LoginProvider = l.LoginProvider, ProviderKey = l.ProviderKey }).ToTable("UserLogins");

            entityTypeConfiguration.HasRequired<IdentityUser>((IdentityUserLogin u) => u.User);
            EntityTypeConfiguration<IdentityUserClaim> table1 = modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");
            table1.HasRequired<IdentityUser>((IdentityUserClaim u) => u.User);

            // Add this, so that IdentityRole can share a table with ApplicationRole:
            modelBuilder.Entity<ApplicationRole>().ToTable("Roles");

            // Change these from IdentityRole to ApplicationRole:
            EntityTypeConfiguration<ApplicationRole> entityTypeConfiguration1 = modelBuilder.Entity<ApplicationRole>().ToTable("Roles");
            entityTypeConfiguration1.Property((ApplicationRole r) => r.Name).IsRequired();

                
        }
    }

}