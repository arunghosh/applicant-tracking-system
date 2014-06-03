namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mile_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Milestones", "MilestoneType", c => c.Int(nullable: false));
            DropColumn("dbo.Projects", "StartDate");
            DropColumn("dbo.Projects", "DueDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "DueDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Milestones", "MilestoneType");
        }
    }
}
