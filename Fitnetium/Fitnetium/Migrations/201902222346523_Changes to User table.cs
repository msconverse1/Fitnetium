namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangestoUsertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "WorkOutType", c => c.String());
            AddColumn("dbo.Users", "age", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "weight", c => c.Single(nullable: false));
            AddColumn("dbo.Users", "hieght", c => c.Single(nullable: false));
            DropColumn("dbo.Users", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            DropColumn("dbo.Users", "hieght");
            DropColumn("dbo.Users", "weight");
            DropColumn("dbo.Users", "age");
            DropColumn("dbo.Users", "WorkOutType");
        }
    }
}
