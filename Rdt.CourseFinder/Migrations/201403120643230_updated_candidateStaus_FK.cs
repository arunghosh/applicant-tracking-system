namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_candidateStaus_FK : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "CandidateStatusId", c => c.Int(nullable: false));
            AddColumn("dbo.CandidateStatus", "CanBeUpdatedBy", c => c.Int(nullable: false));
            AddColumn("dbo.CandidateStatus", "IsUpdateByAll", c => c.Boolean(nullable: false));
            AddColumn("dbo.CandidateStatus", "IsPositive", c => c.Boolean(nullable: false));
            AddForeignKey("dbo.Candidates", "CandidateStatusId", "dbo.CandidateStatus", "CandidateStatusId", cascadeDelete: true);
            CreateIndex("dbo.Candidates", "CandidateStatusId");
            DropColumn("dbo.Candidates", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Candidates", "Status", c => c.String(maxLength: 64));
            DropIndex("dbo.Candidates", new[] { "CandidateStatusId" });
            DropForeignKey("dbo.Candidates", "CandidateStatusId", "dbo.CandidateStatus");
            DropColumn("dbo.CandidateStatus", "IsPositive");
            DropColumn("dbo.CandidateStatus", "IsUpdateByAll");
            DropColumn("dbo.CandidateStatus", "CanBeUpdatedBy");
            DropColumn("dbo.Candidates", "CandidateStatusId");
        }
    }
}
