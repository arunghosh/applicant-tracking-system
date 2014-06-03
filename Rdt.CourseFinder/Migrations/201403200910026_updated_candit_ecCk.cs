namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated_candit_ecCk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "IsEcCheckReq", c => c.Boolean(nullable: false));
            AddColumn("dbo.Candidates", "IsEcCheckDone", c => c.Boolean(nullable: false));
            AddColumn("dbo.Candidates", "StatusUpdatedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "StatusUpdatedAt");
            DropColumn("dbo.Candidates", "IsEcCheckDone");
            DropColumn("dbo.Candidates", "IsEcCheckReq");
        }
    }
}
