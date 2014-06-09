namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_visadetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "VisaIssuesDate", c => c.DateTime());
            AddColumn("dbo.Candidates", "VisaExpiryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "VisaExpiryDate");
            DropColumn("dbo.Candidates", "VisaIssuesDate");
        }
    }
}
