namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_userbranch_prjtype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Branch", c => c.String(maxLength: 64));
            AddColumn("dbo.Projects", "HiringType", c => c.String(maxLength: 64));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "HiringType");
            DropColumn("dbo.Users", "Branch");
        }
    }
}
