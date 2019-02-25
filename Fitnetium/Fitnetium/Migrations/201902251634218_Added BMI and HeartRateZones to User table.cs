namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBMIandHeartRateZonestoUsertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LowHHR", c => c.Double(nullable: false));
            AddColumn("dbo.Users", "HighHHR", c => c.Double(nullable: false));
            AddColumn("dbo.Users", "BMIPercent", c => c.Double(nullable: false));
            AddColumn("dbo.Users", "BMIType", c => c.String());
            AddColumn("dbo.Workouts", "Category", c => c.Int(nullable: false));
            AlterColumn("dbo.Workouts", "Sets", c => c.Int());
            AlterColumn("dbo.Workouts", "Reps", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Workouts", "Reps", c => c.Int(nullable: false));
            AlterColumn("dbo.Workouts", "Sets", c => c.Int(nullable: false));
            DropColumn("dbo.Workouts", "Category");
            DropColumn("dbo.Users", "BMIType");
            DropColumn("dbo.Users", "BMIPercent");
            DropColumn("dbo.Users", "HighHHR");
            DropColumn("dbo.Users", "LowHHR");
        }
    }
}
