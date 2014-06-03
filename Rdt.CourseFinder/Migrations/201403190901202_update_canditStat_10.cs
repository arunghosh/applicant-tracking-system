namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_canditStat_10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CandidateStatus", "Abbrevation", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.CandidateStatus", "Name", c => c.String(nullable: false, maxLength: 32));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CandidateStatus", "Name", c => c.String(maxLength: 32));
            AlterColumn("dbo.CandidateStatus", "Abbrevation", c => c.String(maxLength: 10));
        }
    }
}
