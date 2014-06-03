namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_prjLogs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectLogs",
                c => new
                    {
                        ProjectLogId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 128),
                        ProjectId = c.Int(nullable: false),
                        Log = c.String(maxLength: 512),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectLogId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Imports", "Path", c => c.String());
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProjectLogs", new[] { "ProjectId" });
            DropForeignKey("dbo.ProjectLogs", "ProjectId", "dbo.Projects");
            DropColumn("dbo.Imports", "Path");
            DropTable("dbo.ProjectLogs");
        }
    }
}
