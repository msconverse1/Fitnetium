namespace Fitnetium.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableforMetfromactivity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MetValues",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Activities = c.String(),
                        MET = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MetValues");
        }
    }
}
