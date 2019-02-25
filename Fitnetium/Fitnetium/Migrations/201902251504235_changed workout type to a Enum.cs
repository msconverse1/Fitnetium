namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedworkouttypetoaEnum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "category", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "category");
        }
    }
}
