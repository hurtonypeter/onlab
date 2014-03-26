namespace Perseus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Module",
                c => new
                    {
                        ModuleId = c.Int(nullable: false, identity: true),
                        ModuleName = c.String(),
                    })
                .PrimaryKey(t => t.ModuleId);
            
            CreateTable(
                "dbo.Permission",
                c => new
                    {
                        PermissionId = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        ModuleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PermissionId)
                .ForeignKey("dbo.Module", t => t.ModuleId, cascadeDelete: true)
                .Index(t => t.ModuleId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.IdentityUser_Id)
                .Index(t => t.RoleId)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Status = c.Boolean(),
                        Created = c.DateTime(),
                        LastLogin = c.DateTime(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.IdentityUser_Id)
                .Index(t => t.IdentityUser_Id);
            
            CreateTable(
                "dbo.ApplicationRoleApplicationPermissions",
                c => new
                    {
                        ApplicationRole_Id = c.String(nullable: false, maxLength: 128),
                        ApplicationPermission_PermissionId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationRole_Id, t.ApplicationPermission_PermissionId })
                .ForeignKey("dbo.Role", t => t.ApplicationRole_Id, cascadeDelete: true)
                .ForeignKey("dbo.Permission", t => t.ApplicationPermission_PermissionId, cascadeDelete: true)
                .Index(t => t.ApplicationRole_Id)
                .Index(t => t.ApplicationPermission_PermissionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "IdentityUser_Id", "dbo.User");
            DropForeignKey("dbo.UserLogins", "IdentityUser_Id", "dbo.User");
            DropForeignKey("dbo.UserClaims", "IdentityUser_Id", "dbo.User");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Role");
            DropForeignKey("dbo.ApplicationRoleApplicationPermissions", "ApplicationPermission_PermissionId", "dbo.Permission");
            DropForeignKey("dbo.ApplicationRoleApplicationPermissions", "ApplicationRole_Id", "dbo.Role");
            DropForeignKey("dbo.Permission", "ModuleId", "dbo.Module");
            DropIndex("dbo.ApplicationRoleApplicationPermissions", new[] { "ApplicationPermission_PermissionId" });
            DropIndex("dbo.ApplicationRoleApplicationPermissions", new[] { "ApplicationRole_Id" });
            DropIndex("dbo.UserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserRoles", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.Permission", new[] { "ModuleId" });
            DropTable("dbo.ApplicationRoleApplicationPermissions");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.User");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Role");
            DropTable("dbo.Permission");
            DropTable("dbo.Module");
        }
    }
}
