namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_notification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false, identity: true),
                        CandidateId = c.Int(nullable: false),
                        CandidateName = c.String(),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(),
                        UserRole = c.Int(nullable: false),
                        Message = c.String(maxLength: 512),
                        IsRead = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Candidates", "TravelPostUserId", c => c.Int());
            AddColumn("dbo.Candidates", "PasspostRetUserId", c => c.Int());
        }
        
        public override void Down()
        {
            DropIndex("dbo.Notifications", new[] { "UserId" });
            DropForeignKey("dbo.Notifications", "UserId", "dbo.Users");
            DropColumn("dbo.Candidates", "PasspostRetUserId");
            DropColumn("dbo.Candidates", "TravelPostUserId");
            DropTable("dbo.Notifications");
        }
    }
}
