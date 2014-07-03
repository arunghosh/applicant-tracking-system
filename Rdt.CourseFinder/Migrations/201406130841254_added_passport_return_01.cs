namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_passport_return_01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "PassportReturnReason", c => c.String(maxLength: 512));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "PassportReturnReason");
        }
    }
}
