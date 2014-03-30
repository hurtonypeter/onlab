using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Perseus.DataModel
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser<string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        [Key]
        public override string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastLogin { get; set; }

        /*public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }*/
        
        //public ApplicationUser(string userName)
        //{
        //    UserName = userName;
        //}
        public ApplicationUser() : base()
        {
            
        }

    }

    public class ApplicationUserRole : IdentityUserRole<string> 
    {
        public ApplicationUserRole() : base() { }
    }
    public class ApplicationUserClaim : IdentityUserClaim<string> { }
    public class ApplicationUserLogin : IdentityUserLogin<string> { }


    public class ApplicationRole : IdentityRole<string, ApplicationUserRole>
    {
        public ApplicationRole() : base()
        {
            Permissions = new HashSet<ApplicationPermission>();
        }
        public ApplicationRole(string name) : base()
        {
            this.Name = name;
            this.Permissions = new HashSet<ApplicationPermission>();
        }
        public virtual ICollection<ApplicationPermission> Permissions { get; set; }
    }

    
    public class ApplicationPermission
    {
        [Key]
        public int PermissionId { get; set; }
        public string Name { get; set; }
        public int ModuleId { get; set; }
        public virtual ICollection<ApplicationRole> Roles { get; set; }
        public virtual ApplicationModule Module { get; set; }
        public ApplicationPermission()
        {
            Roles = new HashSet<ApplicationRole>();
        }
    }

    public class ApplicationModule
    {
        [Key]
        public int ModuleId { get; set; }
        public string ModuleName { get; set; }
        public virtual ICollection<ApplicationPermission> Permissions { get; set; }
        public ApplicationModule()
        {
            Permissions = new HashSet<ApplicationPermission>();
        }
    }

    public class ApplicationMenu
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ApplicationMenuItem> MenuLinks { get; set; }
        public ApplicationMenu()
        {
            MenuLinks = new HashSet<ApplicationMenuItem>();
        }

    }
    public class ApplicationMenuItem
    {
        [Key]
        public int Id { get; set; }
        public int MenuId { get; set; }
        public ApplicationMenu Menu { get; set; }
        public int ParentId { get; set; }
        public ApplicationMenuItem Parent { get; set; }
        public ICollection<ApplicationMenuItem> Children { get; set; }
        public string LinkPath { get; set; }
        public string LinkTitle { get; set; }
        public ApplicationPermission Permission { get; set; }
        public ApplicationMenuItem()
        {
            Children = new HashSet<ApplicationMenuItem>();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public virtual IDbSet<ApplicationModule> Modules { get; set; }
        public virtual IDbSet<ApplicationPermission> Permissions { get; set; }
        public virtual IDbSet<ApplicationMenu> Menus { get; set; }
        public virtual IDbSet<ApplicationMenuItem> MenuItems { get; set; }
        //new public virtual IDbSet<ApplicationRole> Roles { get; set; }

        //new public IDbSet<ApplicationUserRole> UserRole { get; set; }
        //public override IDbSet<ApplicationUser> Users { get; set; }
        //new public IDbSet<ApplicationUserClaim> UserClaims { get; set; }
        //public IDbSet<ApplicationUserLogin> UserLogins { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entity = modelBuilder.Entity<ApplicationUser>();
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).HasColumnName("UserId");
            entity.HasMany(u => u.Roles).WithRequired().HasForeignKey(ur => ur.UserId);
            entity.ToTable("User");

            modelBuilder.Entity<ApplicationUserRole>().ToTable("UserRole");

            modelBuilder.Entity<ApplicationRole>().ToTable("Role");

            modelBuilder.Entity<ApplicationPermission>().ToTable("Permission")
                .HasMany(e => e.Roles)
                .WithMany(e => e.Permissions)
                .Map(m => m.ToTable("RolePermission").MapLeftKey("ApplicationPermission_PermissionId").MapRightKey("ApplicationRole_Id"));

            modelBuilder.Entity<ApplicationModule>().ToTable("Module");

            modelBuilder.Entity<ApplicationUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("UserLogin");

            modelBuilder.Entity<ApplicationMenu>().ToTable("Menu")
                .HasMany(e => e.MenuLinks)
                .WithRequired(e => e.Menu)
                .HasForeignKey(e => e.MenuId)
                .WillCascadeOnDelete(true);
            modelBuilder.Entity<ApplicationMenuItem>().ToTable("MenuItem")
                .HasMany(e => e.Children)
                .WithRequired(e => e.Parent)
                .HasForeignKey(e => e.ParentId)
                .WillCascadeOnDelete(false);
        
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    //// Keep this:
        //    //modelBuilder.Entity<IdentityUser>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");

        //    //// Change TUser to ApplicationUser everywhere else - IdentityUser and ApplicationUser essentially 'share' the AspNetUsers Table in the database:
        //    //EntityTypeConfiguration<ApplicationUser> table =
        //    //    modelBuilder.Entity<ApplicationUser>().ToTable("User");
        //    //table.Property(p => p.Id).HasColumnName("UserId");

        //    //table.Property((ApplicationUser u) => u.UserName).IsRequired();

        //    //// EF won't let us swap out IdentityUserRole for ApplicationUserRole here:
        //    ////modelBuilder.Entity<ApplicationUser>().HasMany<IdentityUserRole>((ApplicationUser u) => u.Roles);
        //    //modelBuilder.Entity<IdentityUserRole>().HasKey( r =>
        //    //    new 
        //    //    { 
        //    //        UserId = r.UserId, 
        //    //        RoleId = r.RoleId 
        //    //    }).ToTable("UserRoles");

        //    //// Leave this alone:
        //    //EntityTypeConfiguration<IdentityUserLogin> entityTypeConfiguration =
        //    //    modelBuilder.Entity<IdentityUserLogin>().HasKey((IdentityUserLogin l) =>
        //    //        new
        //    //        {
        //    //            UserId = l.UserId,
        //    //            LoginProvider = l.LoginProvider,
        //    //            ProviderKey = l.ProviderKey
        //    //        }).ToTable("UserLogin");

        //    ////entityTypeConfiguration.HasRequired<IdentityUser>((IdentityUserLogin u) => u.User);
        //    //modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");

        //    //// Add this, so that IdentityRole can share a table with ApplicationRole:
        //    //modelBuilder.Entity<IdentityRole>().ToTable("Role");

        //    //// Change these from IdentityRole to ApplicationRole:
        //    //EntityTypeConfiguration<ApplicationRole> entityTypeConfiguration1 = modelBuilder.Entity<ApplicationRole>().ToTable("Role");
        //    //entityTypeConfiguration1.Property((ApplicationRole r) => r.Name).IsRequired();


        //    //modelBuilder.Entity<ApplicationPermission>().ToTable("Permission")
        //    //    .HasMany(e => e.Roles)
        //    //    .WithMany(e => e.Permissions)
        //    //    .Map(m => m.ToTable("RolePermission").MapLeftKey("Permission_Id").MapRightKey("Role_Id"));

        //    //modelBuilder.Entity<ApplicationModule>().ToTable("Module");



        //    //modelBuilder.Entity<ApplicationPermission>().ToTable("Permission");
        //    //modelBuilder.Entity<ApplicationModule>().ToTable("Module");

        //    //modelBuilder.Entity<ApplicationPermission>()
        //    //    .HasMany(e => e.Roles)
        //    //    .WithMany(e => e.Permissions)
        //    //    .Map(m => m.ToTable("ApplicationRoleApplicationPermissions").MapLeftKey("ApplicationPermission_PermissionId").MapRightKey("ApplicationRole_Id"));

        //    //modelBuilder.Entity<ApplicationRole>()
        //    //    .HasMany(e => e.Users)
        //    //    .WithMany(e => e.)
        //    //    .Map(m => m.ToTable("UserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

        //    //modelBuilder.Entity<ApplicationUser>()
        //    //    .HasMany(e => e.Claims)
        //    //    .WithRequired(e => e.User)
        //    //    .WillCascadeOnDelete(false);

        //    //modelBuilder.Entity<ApplicationUser>()
        //    //    .HasMany(e => e.Logins)
        //    //    .WithRequired(e => e.UserId)
        //    //    .WillCascadeOnDelete(false);

        //}

        //működött már eccer
        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");
            modelBuilder.Entity<ApplicationUser>().ToTable("User").Property(p => p.Id).HasColumnName("UserId");

            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");

            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<ApplicationRole>().ToTable("Role")
                .HasMany(e => e.Users);

            modelBuilder.Entity<ApplicationPermission>().ToTable("Permission");
            modelBuilder.Entity<ApplicationModule>().ToTable("Module");
        }*/

       /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
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
           /* modelBuilder.Entity<IdentityUser>().ToTable("Users");

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
           /* modelBuilder.Entity<ApplicationPermission>().HasMany<ApplicationRolePermisson>((ApplicationPermission g) => g.Roles);
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

            
        }*/
    }

    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, string, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUserStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
    public class ApplicationRoleStore : RoleStore<ApplicationRole, string, ApplicationUserRole>
    {
        public ApplicationRoleStore(ApplicationDbContext context)
            : base(context)
        {
        }
    }
    public class ApplicationUserManager : UserManager<ApplicationUser, string>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser, string> store)
            : base(store)
        {

        }

        // Create method called once during the lifecycle of request
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            // Configure the userstore to use the DbContext to work with database
            var manager = new ApplicationUserManager(new ApplicationUserStore(context.Get<ApplicationDbContext>()));

            // The password validator enforces complexity on supplied password
            manager.PasswordValidator = new PasswordValidator()
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true
            };

            // Use the custom password hasher to validate existing user credentials
            manager.PasswordHasher = new AppPasswordHasher() { DbContext = context.Get<ApplicationDbContext>() };

            return manager;
        }
    }
    public class ApplicationRoleManager : RoleManager<ApplicationRole, string>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store)
            : base(store)
        {

        }
    }

    public class AppPasswordHasher : PasswordHasher
    {
        public ApplicationDbContext DbContext { get; set; }

        // Custom hashing used before migrating to Identity
        public static string GetMD5Hash(string value)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        // Verify if the password is hashed using MD5. If yes, rehash using ASP.NET Identity Crypto which is more secure
        // this is invoked when old users try to login. Eventually all the old the password are rehashed to a more secure hash
        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (String.Equals(hashedPassword, GetMD5Hash(providedPassword), StringComparison.InvariantCultureIgnoreCase))
            {
                ReHashPassword(hashedPassword, providedPassword);
                return PasswordVerificationResult.Success;
            }

            return base.VerifyHashedPassword(hashedPassword, providedPassword);
        }

        // Rehash password using ASP.NET Identity Crypto
        // Store it back into database
        private void ReHashPassword(string hashedPassword, string providedPassword)
        {

            var user = DbContext.Users.Where(x => x.PasswordHash == hashedPassword).FirstOrDefault();

            user.PasswordHash = base.HashPassword(providedPassword);

            // Update SecurityStamp with new Guid to nullify any previous cookies
            user.SecurityStamp = Guid.NewGuid().ToString();

            DbContext.SaveChanges();
        }
    }
}
