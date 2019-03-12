namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateingIntensity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workouts", "WorkoutCategory", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Workouts", "WorkoutCategory");
        }
    }
}
