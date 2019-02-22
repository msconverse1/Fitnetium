namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeworkoutidinusernullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "UserWorkoutID", "dbo.Workouts");
            DropIndex("dbo.Users", new[] { "UserWorkoutID" });
            AlterColumn("dbo.Users", "UserWorkoutID", c => c.Int());
            CreateIndex("dbo.Users", "UserWorkoutID");
            AddForeignKey("dbo.Users", "UserWorkoutID", "dbo.Workouts", "UserWorkoutID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserWorkoutID", "dbo.Workouts");
            DropIndex("dbo.Users", new[] { "UserWorkoutID" });
            AlterColumn("dbo.Users", "UserWorkoutID", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "UserWorkoutID");
            AddForeignKey("dbo.Users", "UserWorkoutID", "dbo.Workouts", "UserWorkoutID", cascadeDelete: true);
        }
    }
}
