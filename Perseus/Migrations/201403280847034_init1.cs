namespace Perseus.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Permisson", newName: "Permission");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Permission", newName: "Permisson");
        }
    }
}
