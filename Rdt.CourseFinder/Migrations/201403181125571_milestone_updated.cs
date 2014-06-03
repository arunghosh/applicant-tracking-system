namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class milestone_updated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Milestones", "CandidateStatusId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Milestones", "CandidateStatusId");
        }
    }
}
