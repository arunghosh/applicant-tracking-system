namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_sessions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        UserSessionId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        SessionId = c.String(maxLength: 32),
                        IPAddress = c.String(maxLength: 32),
                        Browser = c.String(maxLength: 64),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(),
                        UserId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserSessionId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sessions", new[] { "UserId" });
            DropForeignKey("dbo.Sessions", "UserId", "dbo.Users");
            DropTable("dbo.Sessions");
        }
    }
}
