namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class medical_details : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "MedicalDoneDate", c => c.DateTime());
            AddColumn("dbo.Candidates", "MedicalExpiryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "MedicalExpiryDate");
            DropColumn("dbo.Candidates", "MedicalDoneDate");
        }
    }
}
