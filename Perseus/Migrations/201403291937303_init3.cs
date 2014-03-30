namespace Perseus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuItem", "Menu_Id", "dbo.Menu");
            DropIndex("dbo.MenuItem", new[] { "ApplicationMenuItem_Id" });
            DropIndex("dbo.MenuItem", new[] { "Menu_Id" });
            RenameColumn(table: "dbo.MenuItem", name: "ApplicationMenuItem_Id", newName: "ParentId");
            RenameColumn(table: "dbo.MenuItem", name: "Menu_Id", newName: "MenuId");
            AlterColumn("dbo.MenuItem", "ParentId", c => c.Int(nullable: false));
            AlterColumn("dbo.MenuItem", "MenuId", c => c.Int(nullable: false));
            CreateIndex("dbo.MenuItem", "MenuId");
            CreateIndex("dbo.MenuItem", "ParentId");
            AddForeignKey("dbo.MenuItem", "MenuId", "dbo.Menu", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuItem", "MenuId", "dbo.Menu");
            DropIndex("dbo.MenuItem", new[] { "ParentId" });
            DropIndex("dbo.MenuItem", new[] { "MenuId" });
            AlterColumn("dbo.MenuItem", "MenuId", c => c.Int());
            AlterColumn("dbo.MenuItem", "ParentId", c => c.Int());
            RenameColumn(table: "dbo.MenuItem", name: "MenuId", newName: "Menu_Id");
            RenameColumn(table: "dbo.MenuItem", name: "ParentId", newName: "ApplicationMenuItem_Id");
            CreateIndex("dbo.MenuItem", "Menu_Id");
            CreateIndex("dbo.MenuItem", "ApplicationMenuItem_Id");
            AddForeignKey("dbo.MenuItem", "Menu_Id", "dbo.Menu", "Id");
        }
    }
}
