namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Password = c.String(nullable: false, maxLength: 256),
                        Name = c.String(nullable: false, maxLength: 64),
                        Email = c.String(nullable: false, maxLength: 128),
                        Mobile = c.String(maxLength: 32),
                        Role = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserRoleId = c.Int(nullable: false, identity: true),
                        RoleType = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserRoleId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogs",
                c => new
                    {
                        UserLogId = c.Int(nullable: false, identity: true),
                        Comment = c.String(maxLength: 1024),
                        ByUserId = c.Int(nullable: false),
                        ByUserName = c.String(maxLength: 128),
                        UserId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserLogId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        CandidateId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Passport = c.String(),
                        Category = c.String(),
                        Age = c.Int(nullable: false),
                        City = c.String(),
                        Remarks = c.String(),
                        Grade = c.String(),
                        Status = c.String(),
                        Company = c.String(),
                    })
                .PrimaryKey(t => t.CandidateId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        ProjectName = c.String(nullable: false, maxLength: 128),
                        StartDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        CompanyId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CompanyId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        CompanyId = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(nullable: false, maxLength: 256),
                        Country = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CompanyId);
            
            CreateTable(
                "dbo.Requirements",
                c => new
                    {
                        RequirementId = c.Int(nullable: false, identity: true),
                        Category = c.String(maxLength: 128),
                        Count = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ProjectId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RequirementId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        LogMessage = c.String(maxLength: 256),
                        UserId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        IPAddress = c.String(maxLength: 32),
                        IsRead = c.Boolean(nullable: false),
                        LogType = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LogId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Requirements", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "UserId" });
            DropIndex("dbo.Projects", new[] { "CompanyId" });
            DropIndex("dbo.UserLogs", new[] { "UserId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropForeignKey("dbo.Requirements", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "UserId", "dbo.Users");
            DropForeignKey("dbo.Projects", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.UserLogs", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropTable("dbo.Logs");
            DropTable("dbo.Requirements");
            DropTable("dbo.Companies");
            DropTable("dbo.Projects");
            DropTable("dbo.Candidates");
            DropTable("dbo.UserLogs");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
        }
    }
}
