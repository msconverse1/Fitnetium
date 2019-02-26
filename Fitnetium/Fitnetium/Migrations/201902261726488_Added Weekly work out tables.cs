namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedWeeklyworkouttables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fridays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Sets = c.Int(),
                        Reps = c.Int(),
                        Weight = c.Single(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Mondays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Sets = c.Int(),
                        Reps = c.Int(),
                        Weight = c.Single(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Saturdays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Sets = c.Int(),
                        Reps = c.Int(),
                        Weight = c.Single(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Sundays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Sets = c.Int(),
                        Reps = c.Int(),
                        Weight = c.Single(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Tuesdays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Sets = c.Int(),
                        Reps = c.Int(),
                        Weight = c.Single(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Wednesdays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Sets = c.Int(),
                        Reps = c.Int(),
                        Weight = c.Single(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID)
                .Index(t => t.UserID);
            
            DropColumn("dbo.Workouts", "Equipent");
            DropColumn("dbo.Workouts", "MainMuscles");
            DropColumn("dbo.Workouts", "SencMuscles");
            DropColumn("dbo.Workouts", "Sets");
            DropColumn("dbo.Workouts", "Reps");
            DropColumn("dbo.Workouts", "Weight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workouts", "Weight", c => c.Single());
            AddColumn("dbo.Workouts", "Reps", c => c.Int());
            AddColumn("dbo.Workouts", "Sets", c => c.Int());
            AddColumn("dbo.Workouts", "SencMuscles", c => c.String());
            AddColumn("dbo.Workouts", "MainMuscles", c => c.String());
            AddColumn("dbo.Workouts", "Equipent", c => c.String());
            DropForeignKey("dbo.Wednesdays", "UserID", "dbo.Users");
            DropForeignKey("dbo.Tuesdays", "UserID", "dbo.Users");
            DropForeignKey("dbo.Sundays", "UserID", "dbo.Users");
            DropForeignKey("dbo.Saturdays", "UserID", "dbo.Users");
            DropForeignKey("dbo.Mondays", "UserID", "dbo.Users");
            DropForeignKey("dbo.Fridays", "UserID", "dbo.Users");
            DropIndex("dbo.Wednesdays", new[] { "UserID" });
            DropIndex("dbo.Tuesdays", new[] { "UserID" });
            DropIndex("dbo.Sundays", new[] { "UserID" });
            DropIndex("dbo.Saturdays", new[] { "UserID" });
            DropIndex("dbo.Mondays", new[] { "UserID" });
            DropIndex("dbo.Fridays", new[] { "UserID" });
            DropTable("dbo.Wednesdays");
            DropTable("dbo.Tuesdays");
            DropTable("dbo.Sundays");
            DropTable("dbo.Saturdays");
            DropTable("dbo.Mondays");
            DropTable("dbo.Fridays");
        }
    }
}
