namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_CanditStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandidateStatus",
                c => new
                    {
                        CandidateStatusId = c.Int(nullable: false, identity: true),
                        Abbrevation = c.String(maxLength: 10),
                        Name = c.String(maxLength: 32),
                        IsDeleted = c.Boolean(nullable: false),
                        Index = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CandidateStatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CandidateStatus");
        }
    }
}
