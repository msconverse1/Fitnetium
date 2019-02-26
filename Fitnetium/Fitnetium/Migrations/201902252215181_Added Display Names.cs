namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDisplayNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workouts", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Workouts", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workouts", "EndDate");
            DropColumn("dbo.Workouts", "StartDate");
        }
    }
}
