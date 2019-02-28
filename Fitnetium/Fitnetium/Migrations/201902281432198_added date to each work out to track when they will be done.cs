namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeddatetoeachworkouttotrackwhentheywillbedone : DbMigration
    {
        public override void Up()
        {


            AddColumn("dbo.Workouts", "Category2", c => c.Int(nullable: false));
            AddColumn("dbo.Workouts", "Category3", c => c.Int(nullable: false));
            AddColumn("dbo.Workouts", "Category4", c => c.Int(nullable: false));
            AddColumn("dbo.Workouts", "Category5", c => c.Int(nullable: false));
            AddColumn("dbo.Workouts", "Category6", c => c.Int(nullable: false));
            AddColumn("dbo.Workouts", "Category7", c => c.Int(nullable: false));
            AddColumn("dbo.Mondays", "Date", c => c.DateTime(nullable: false));

        }
        
        public override void Down()
        {

            DropColumn("dbo.Mondays", "Date");
            DropColumn("dbo.Workouts", "Category7");
            DropColumn("dbo.Workouts", "Category6");
            DropColumn("dbo.Workouts", "Category5");
            DropColumn("dbo.Workouts", "Category4");
            DropColumn("dbo.Workouts", "Category3");
            DropColumn("dbo.Workouts", "Category2");
        }
    }
}
