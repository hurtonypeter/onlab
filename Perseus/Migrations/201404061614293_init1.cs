namespace Perseus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuId = c.Int(nullable: false),
                        ParentId = c.Int(),
                        LinkPath = c.String(),
                        LinkTitle = c.String(),
                        Permission_PermissionId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuItem", t => t.ParentId)
                .ForeignKey("dbo.Menu", t => t.MenuId, cascadeDelete: true)
                .ForeignKey("dbo.Permission", t => t.Permission_PermissionId)
                .Index(t => t.MenuId)
                .Index(t => t.ParentId)
                .Index(t => t.Permission_PermissionId);
            
            CreateTable(
                "dbo.Menu",
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
            DropForeignKey("dbo.MenuItem", "Permission_PermissionId", "dbo.Permission");
            DropForeignKey("dbo.MenuItem", "MenuId", "dbo.Menu");
            DropForeignKey("dbo.MenuItem", "ParentId", "dbo.MenuItem");
            DropIndex("dbo.MenuItem", new[] { "Permission_PermissionId" });
            DropIndex("dbo.MenuItem", new[] { "ParentId" });
            DropIndex("dbo.MenuItem", new[] { "MenuId" });
            DropTable("dbo.Menu");
            DropTable("dbo.MenuItem");
        }
    }
}
