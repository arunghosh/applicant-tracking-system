namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_visa_deposit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "IsVisaDeposited", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "IsVisaDeposited");
        }
    }
}
