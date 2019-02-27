namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatedtablestohavemetvaluesandcaloriesperworkout : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fridays", "UserID", "dbo.Users");
            DropForeignKey("dbo.Mondays", "UserID", "dbo.Users");
            DropForeignKey("dbo.Saturdays", "UserID", "dbo.Users");
            DropForeignKey("dbo.Sundays", "UserID", "dbo.Users");
            DropForeignKey("dbo.Thursdays", "UserID", "dbo.Users");
            DropForeignKey("dbo.Tuesdays", "UserID", "dbo.Users");
            DropForeignKey("dbo.Wednesdays", "UserID", "dbo.Users");
            DropIndex("dbo.Fridays", new[] { "UserID" });
            DropIndex("dbo.Mondays", new[] { "UserID" });
            DropIndex("dbo.Saturdays", new[] { "UserID" });
            DropIndex("dbo.Sundays", new[] { "UserID" });
            DropIndex("dbo.Thursdays", new[] { "UserID" });
            DropIndex("dbo.Tuesdays", new[] { "UserID" });
            DropIndex("dbo.Wednesdays", new[] { "UserID" });
            AddColumn("dbo.Fridays", "WorkoutID", c => c.Int());
            AddColumn("dbo.Fridays", "CaloriesBurned", c => c.Double(nullable: false));
            AddColumn("dbo.Fridays", "Met", c => c.Int(nullable: false));
            AddColumn("dbo.Mondays", "Met", c => c.Double());
            AddColumn("dbo.Mondays", "CaloriesBurned", c => c.Double());
            AddColumn("dbo.Mondays", "WorkoutID", c => c.Int());
            AddColumn("dbo.Saturdays", "WorkoutID", c => c.Int());
            AddColumn("dbo.Saturdays", "CaloriesBurned", c => c.Double(nullable: false));
            AddColumn("dbo.Saturdays", "Met", c => c.Int(nullable: false));
            AddColumn("dbo.Sundays", "WorkoutID", c => c.Int());
            AddColumn("dbo.Sundays", "CaloriesBurned", c => c.Double(nullable: false));
            AddColumn("dbo.Sundays", "Met", c => c.Int(nullable: false));
            AddColumn("dbo.Thursdays", "WorkoutID", c => c.Int());
            AddColumn("dbo.Thursdays", "Met", c => c.Int(nullable: false));
            AddColumn("dbo.Thursdays", "CaloriesBurned", c => c.Double(nullable: false));
            AddColumn("dbo.Tuesdays", "WorkoutID", c => c.Int());
            AddColumn("dbo.Tuesdays", "Met", c => c.Int(nullable: false));
            AddColumn("dbo.Tuesdays", "CaloriesBurned", c => c.Double(nullable: false));
            AddColumn("dbo.Wednesdays", "WorkoutID", c => c.Int());
            AddColumn("dbo.Wednesdays", "Met", c => c.Int(nullable: false));
            AddColumn("dbo.Wednesdays", "CaloriesBurned", c => c.Double(nullable: false));
            CreateIndex("dbo.Fridays", "WorkoutID");
            CreateIndex("dbo.Mondays", "WorkoutID");
            CreateIndex("dbo.Saturdays", "WorkoutID");
            CreateIndex("dbo.Sundays", "WorkoutID");
            CreateIndex("dbo.Thursdays", "WorkoutID");
            CreateIndex("dbo.Tuesdays", "WorkoutID");
            CreateIndex("dbo.Wednesdays", "WorkoutID");
            AddForeignKey("dbo.Fridays", "WorkoutID", "dbo.Workouts", "UserWorkoutID");
            AddForeignKey("dbo.Mondays", "WorkoutID", "dbo.Workouts", "UserWorkoutID");
            AddForeignKey("dbo.Saturdays", "WorkoutID", "dbo.Workouts", "UserWorkoutID");
            AddForeignKey("dbo.Sundays", "WorkoutID", "dbo.Workouts", "UserWorkoutID");
            AddForeignKey("dbo.Thursdays", "WorkoutID", "dbo.Workouts", "UserWorkoutID");
            AddForeignKey("dbo.Tuesdays", "WorkoutID", "dbo.Workouts", "UserWorkoutID");
            AddForeignKey("dbo.Wednesdays", "WorkoutID", "dbo.Workouts", "UserWorkoutID");
            DropColumn("dbo.Fridays", "UserID");
            DropColumn("dbo.Mondays", "UserID");
            DropColumn("dbo.Saturdays", "UserID");
            DropColumn("dbo.Sundays", "UserID");
            DropColumn("dbo.Thursdays", "UserID");
            DropColumn("dbo.Tuesdays", "UserID");
            DropColumn("dbo.Wednesdays", "UserID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Wednesdays", "UserID", c => c.Int());
            AddColumn("dbo.Tuesdays", "UserID", c => c.Int());
            AddColumn("dbo.Thursdays", "UserID", c => c.Int());
            AddColumn("dbo.Sundays", "UserID", c => c.Int());
            AddColumn("dbo.Saturdays", "UserID", c => c.Int());
            AddColumn("dbo.Mondays", "UserID", c => c.Int());
            AddColumn("dbo.Fridays", "UserID", c => c.Int());
            DropForeignKey("dbo.Wednesdays", "WorkoutID", "dbo.Workouts");
            DropForeignKey("dbo.Tuesdays", "WorkoutID", "dbo.Workouts");
            DropForeignKey("dbo.Thursdays", "WorkoutID", "dbo.Workouts");
            DropForeignKey("dbo.Sundays", "WorkoutID", "dbo.Workouts");
            DropForeignKey("dbo.Saturdays", "WorkoutID", "dbo.Workouts");
            DropForeignKey("dbo.Mondays", "WorkoutID", "dbo.Workouts");
            DropForeignKey("dbo.Fridays", "WorkoutID", "dbo.Workouts");
            DropIndex("dbo.Wednesdays", new[] { "WorkoutID" });
            DropIndex("dbo.Tuesdays", new[] { "WorkoutID" });
            DropIndex("dbo.Thursdays", new[] { "WorkoutID" });
            DropIndex("dbo.Sundays", new[] { "WorkoutID" });
            DropIndex("dbo.Saturdays", new[] { "WorkoutID" });
            DropIndex("dbo.Mondays", new[] { "WorkoutID" });
            DropIndex("dbo.Fridays", new[] { "WorkoutID" });
            DropColumn("dbo.Wednesdays", "CaloriesBurned");
            DropColumn("dbo.Wednesdays", "Met");
            DropColumn("dbo.Wednesdays", "WorkoutID");
            DropColumn("dbo.Tuesdays", "CaloriesBurned");
            DropColumn("dbo.Tuesdays", "Met");
            DropColumn("dbo.Tuesdays", "WorkoutID");
            DropColumn("dbo.Thursdays", "CaloriesBurned");
            DropColumn("dbo.Thursdays", "Met");
            DropColumn("dbo.Thursdays", "WorkoutID");
            DropColumn("dbo.Sundays", "Met");
            DropColumn("dbo.Sundays", "CaloriesBurned");
            DropColumn("dbo.Sundays", "WorkoutID");
            DropColumn("dbo.Saturdays", "Met");
            DropColumn("dbo.Saturdays", "CaloriesBurned");
            DropColumn("dbo.Saturdays", "WorkoutID");
            DropColumn("dbo.Mondays", "WorkoutID");
            DropColumn("dbo.Mondays", "CaloriesBurned");
            DropColumn("dbo.Mondays", "Met");
            DropColumn("dbo.Fridays", "Met");
            DropColumn("dbo.Fridays", "CaloriesBurned");
            DropColumn("dbo.Fridays", "WorkoutID");
            CreateIndex("dbo.Wednesdays", "UserID");
            CreateIndex("dbo.Tuesdays", "UserID");
            CreateIndex("dbo.Thursdays", "UserID");
            CreateIndex("dbo.Sundays", "UserID");
            CreateIndex("dbo.Saturdays", "UserID");
            CreateIndex("dbo.Mondays", "UserID");
            CreateIndex("dbo.Fridays", "UserID");
            AddForeignKey("dbo.Wednesdays", "UserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Tuesdays", "UserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Thursdays", "UserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Sundays", "UserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Saturdays", "UserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Mondays", "UserID", "dbo.Users", "ID");
            AddForeignKey("dbo.Fridays", "UserID", "dbo.Users", "ID");
        }
    }
}
