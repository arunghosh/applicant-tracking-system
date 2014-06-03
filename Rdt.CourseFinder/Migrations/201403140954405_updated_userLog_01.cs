namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_userLog_01 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserLogs", "Comment", c => c.String(maxLength: 2048));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserLogs", "Comment", c => c.String(maxLength: 1024));
        }
    }
}
