namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_Candit_10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "Bsro", c => c.String(maxLength: 32));
            AddColumn("dbo.Candidates", "SeleDate", c => c.String(maxLength: 32));
            AddColumn("dbo.Candidates", "ContactNo", c => c.String(maxLength: 32));
            AddColumn("dbo.Candidates", "Experience", c => c.String(maxLength: 64));
            AddColumn("dbo.Candidates", "Agent", c => c.String(maxLength: 64));
            AlterColumn("dbo.Candidates", "Name", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Candidates", "Category", c => c.String(maxLength: 64));
            AlterColumn("dbo.Candidates", "Age", c => c.String(maxLength: 10));
            AlterColumn("dbo.Candidates", "City", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Candidates", "City", c => c.String(maxLength: 128));
            AlterColumn("dbo.Candidates", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.Candidates", "Category", c => c.String(maxLength: 128));
            AlterColumn("dbo.Candidates", "Name", c => c.String(maxLength: 256));
            DropColumn("dbo.Candidates", "Agent");
            DropColumn("dbo.Candidates", "Experience");
            DropColumn("dbo.Candidates", "ContactNo");
            DropColumn("dbo.Candidates", "SeleDate");
            DropColumn("dbo.Candidates", "Bsro");
        }
    }
}
