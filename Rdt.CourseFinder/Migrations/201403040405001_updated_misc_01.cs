namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_misc_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Candidates", "Name", c => c.String(maxLength: 256));
            AlterColumn("dbo.Candidates", "Passport", c => c.String(maxLength: 64));
            AlterColumn("dbo.Candidates", "Category", c => c.String(maxLength: 128));
            AlterColumn("dbo.Candidates", "City", c => c.String(maxLength: 128));
            AlterColumn("dbo.Candidates", "Remarks", c => c.String(maxLength: 256));
            AlterColumn("dbo.Candidates", "Grade", c => c.String(maxLength: 64));
            DropColumn("dbo.Candidates", "Company");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Candidates", "Company", c => c.String());
            AlterColumn("dbo.Candidates", "Grade", c => c.String());
            AlterColumn("dbo.Candidates", "Remarks", c => c.String());
            AlterColumn("dbo.Candidates", "City", c => c.String());
            AlterColumn("dbo.Candidates", "Category", c => c.String());
            AlterColumn("dbo.Candidates", "Passport", c => c.String());
            AlterColumn("dbo.Candidates", "Name", c => c.String());
        }
    }
}
