namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MetValuesaddedIntensity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MetValues", "Intensity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MetValues", "Intensity");
        }
    }
}
