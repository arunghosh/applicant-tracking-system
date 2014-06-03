namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_traveldetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "Airlines", c => c.String(maxLength: 128));
            AddColumn("dbo.Candidates", "TravelDate", c => c.DateTime());
            AddColumn("dbo.Candidates", "BoardingCity", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "BoardingCity");
            DropColumn("dbo.Candidates", "TravelDate");
            DropColumn("dbo.Candidates", "Airlines");
        }
    }
}
