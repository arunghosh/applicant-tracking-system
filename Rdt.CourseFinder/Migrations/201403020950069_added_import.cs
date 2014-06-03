namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_import : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Imports",
                c => new
                    {
                        ImportId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        UserName = c.String(maxLength: 128),
                        ProjectId = c.Int(nullable: false),
                        SuccessCount = c.Int(nullable: false),
                        TotalCount = c.Int(nullable: false),
                        Remark = c.String(maxLength: 256),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ImportId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Candidates", "ImportId", c => c.Int(nullable: false));
            AddColumn("dbo.Candidates", "CreatedOn", c => c.DateTime(nullable: false));
            AddForeignKey("dbo.Candidates", "ImportId", "dbo.Imports", "ImportId", cascadeDelete: true);
            CreateIndex("dbo.Candidates", "ImportId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Imports", new[] { "ProjectId" });
            DropIndex("dbo.Candidates", new[] { "ImportId" });
            DropForeignKey("dbo.Imports", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Candidates", "ImportId", "dbo.Imports");
            DropColumn("dbo.Candidates", "CreatedOn");
            DropColumn("dbo.Candidates", "ImportId");
            DropTable("dbo.Imports");
        }
    }
}
