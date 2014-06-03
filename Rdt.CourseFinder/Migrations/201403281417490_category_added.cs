namespace Rdt.CourseFinder.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class category_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 32),
                        IsDeleted = c.Boolean(nullable: false),
                        MasterCategoryId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId)
                .ForeignKey("dbo.MasterCategories", t => t.MasterCategoryId, cascadeDelete: true)
                .Index(t => t.MasterCategoryId);
            
            CreateTable(
                "dbo.MasterCategories",
                c => new
                    {
                        MasterCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 32),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MasterCategoryId);
            
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        SubCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 32),
                        CategoryId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SubCategoryId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.SubCategories", new[] { "CategoryId" });
            DropIndex("dbo.Categories", new[] { "MasterCategoryId" });
            DropForeignKey("dbo.SubCategories", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Categories", "MasterCategoryId", "dbo.MasterCategories");
            DropTable("dbo.SubCategories");
            DropTable("dbo.MasterCategories");
            DropTable("dbo.Categories");
        }
    }
}
