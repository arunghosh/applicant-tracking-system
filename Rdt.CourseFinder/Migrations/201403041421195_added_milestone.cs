namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_milestone : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Candidates", "ImportId", "dbo.Imports");
            DropIndex("dbo.Candidates", new[] { "ImportId" });
            CreateTable(
                "dbo.Milestones",
                c => new
                    {
                        MilestoneId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        ExpectedDate = c.DateTime(nullable: false),
                        ActualDate = c.DateTime(),
                        ProjectId = c.Int(nullable: false),
                        Comments = c.String(maxLength: 512),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MilestoneId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            AddColumn("dbo.Candidates", "ProjectId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Candidates", "ProjectId", "dbo.Projects", "ProjectId", cascadeDelete: true);
            CreateIndex("dbo.Candidates", "ProjectId");
            DropColumn("dbo.Candidates", "ImportId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Candidates", "ImportId", c => c.Int(nullable: false));
            DropIndex("dbo.Milestones", new[] { "ProjectId" });
            DropIndex("dbo.Candidates", new[] { "ProjectId" });
            DropForeignKey("dbo.Milestones", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Candidates", "ProjectId", "dbo.Projects");
            DropColumn("dbo.Candidates", "ProjectId");
            DropTable("dbo.Milestones");
            CreateIndex("dbo.Candidates", "ImportId");
            AddForeignKey("dbo.Candidates", "ImportId", "dbo.Imports", "ImportId", cascadeDelete: true);
        }
    }
}
