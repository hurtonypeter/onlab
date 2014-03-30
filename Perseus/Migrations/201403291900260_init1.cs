namespace Perseus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationMenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LinkPath = c.String(),
                        LinkTitle = c.String(),
                        ApplicationMenuItem_Id = c.Int(),
                        Menu_Id = c.Int(),
                        Permission_PermissionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationMenuItems", t => t.ApplicationMenuItem_Id)
                .ForeignKey("dbo.ApplicationMenus", t => t.Menu_Id)
                .ForeignKey("dbo.Permission", t => t.Permission_PermissionId)
                .Index(t => t.ApplicationMenuItem_Id)
                .Index(t => t.Menu_Id)
                .Index(t => t.Permission_PermissionId);
            
            CreateTable(
                "dbo.ApplicationMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationMenuItems", "Permission_PermissionId", "dbo.Permission");
            DropForeignKey("dbo.ApplicationMenuItems", "Menu_Id", "dbo.ApplicationMenus");
            DropForeignKey("dbo.ApplicationMenuItems", "ApplicationMenuItem_Id", "dbo.ApplicationMenuItems");
            DropIndex("dbo.ApplicationMenuItems", new[] { "Permission_PermissionId" });
            DropIndex("dbo.ApplicationMenuItems", new[] { "Menu_Id" });
            DropIndex("dbo.ApplicationMenuItems", new[] { "ApplicationMenuItem_Id" });
            DropTable("dbo.ApplicationMenus");
            DropTable("dbo.ApplicationMenuItems");
        }
    }
}
