namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedFKtoRecipe : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "WorkoutID", c => c.Int());
            CreateIndex("dbo.Recipes", "WorkoutID");
            AddForeignKey("dbo.Recipes", "WorkoutID", "dbo.Workouts", "UserWorkoutID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "WorkoutID", "dbo.Workouts");
            DropIndex("dbo.Recipes", new[] { "WorkoutID" });
            DropColumn("dbo.Recipes", "WorkoutID");
        }
    }
}
