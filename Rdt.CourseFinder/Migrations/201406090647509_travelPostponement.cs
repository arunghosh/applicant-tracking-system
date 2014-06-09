namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class travelPostponement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "TravelPostponement", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "TravelPostponement");
        }
    }
}
