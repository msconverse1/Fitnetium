namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedthursdaytotable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Thursdays",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Thursdays", "UserID", "dbo.Users");
            DropIndex("dbo.Thursdays", new[] { "UserID" });
            DropTable("dbo.Thursdays");
        }
    }
}
