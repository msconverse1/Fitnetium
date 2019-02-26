namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFKtoUserfordifferentworkoutseveryday : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UserWorkoutID", "dbo.Workouts");
            DropIndex("dbo.Users", new[] { "UserWorkoutID" });
            AddColumn("dbo.Workouts", "UserID", c => c.Int());
            CreateIndex("dbo.Workouts", "UserID");
            AddForeignKey("dbo.Workouts", "UserID", "dbo.Users", "ID");
            DropColumn("dbo.Users", "UserWorkoutID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserWorkoutID", c => c.Int());
            DropForeignKey("dbo.Workouts", "UserID", "dbo.Users");
            DropIndex("dbo.Workouts", new[] { "UserID" });
            DropColumn("dbo.Workouts", "UserID");
            CreateIndex("dbo.Users", "UserWorkoutID");
            AddForeignKey("dbo.Users", "UserWorkoutID", "dbo.Workouts", "UserWorkoutID");
        }
    }
}
