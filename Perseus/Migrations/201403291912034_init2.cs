namespace Perseus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationMenuItems", newName: "MenuItem");
            RenameTable(name: "dbo.ApplicationMenus", newName: "Menu");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Menu", newName: "ApplicationMenus");
            RenameTable(name: "dbo.MenuItem", newName: "ApplicationMenuItems");
        }
    }
}
