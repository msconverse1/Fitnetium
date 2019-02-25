namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHeightSettoUsertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Heightset", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Heightset");
        }
    }
}
