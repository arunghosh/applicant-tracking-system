namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_canditLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandidateLogs",
                c => new
                    {
                        CandidateLogId = c.Int(nullable: false, identity: true),
                        ByUserId = c.Int(nullable: false),
                        ByUserName = c.String(maxLength: 128),
                        CandidateId = c.Int(nullable: false),
                        Log = c.String(maxLength: 512),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateLogId)
                .ForeignKey("dbo.Candidates", t => t.CandidateId, cascadeDelete: true)
                .Index(t => t.CandidateId);
            
            AddColumn("dbo.Candidates", "CreationType", c => c.Int(nullable: false));
            AddColumn("dbo.Candidates", "IsDeleted", c => c.Int(nullable: false));
            AlterColumn("dbo.Candidates", "Status", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            DropIndex("dbo.CandidateLogs", new[] { "CandidateId" });
            DropForeignKey("dbo.CandidateLogs", "CandidateId", "dbo.Candidates");
            AlterColumn("dbo.Candidates", "Status", c => c.String());
            DropColumn("dbo.Candidates", "IsDeleted");
            DropColumn("dbo.Candidates", "CreationType");
            DropTable("dbo.CandidateLogs");
        }
    }
}
