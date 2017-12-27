namespace CMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class f : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuItems", "Template_ID", "dbo.PageTemplates");
            DropIndex("dbo.MenuItems", new[] { "Template_ID" });
            AlterColumn("dbo.MenuItems", "Template_Id", c => c.Int(nullable: false));
            AddForeignKey("dbo.MenuItems", "Template_Id", "dbo.PageTemplates", "ID", cascadeDelete: true);
            CreateIndex("dbo.MenuItems", "Template_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.MenuItems", new[] { "Template_Id" });
            DropForeignKey("dbo.MenuItems", "Template_Id", "dbo.PageTemplates");
            AlterColumn("dbo.MenuItems", "Template_ID", c => c.Int());
            CreateIndex("dbo.MenuItems", "Template_ID");
            AddForeignKey("dbo.MenuItems", "Template_ID", "dbo.PageTemplates", "ID");
        }
    }
}
