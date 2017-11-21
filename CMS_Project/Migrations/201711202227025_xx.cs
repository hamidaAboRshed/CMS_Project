namespace CMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xx : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.item_lang",
                c => new
                    {
                        item_ID = c.Int(nullable: false),
                        Lang_ID = c.Int(nullable: false),
                        Title = c.String(),
                        Content = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.item_ID, t.Lang_ID })
                .ForeignKey("dbo.Languages", t => t.Lang_ID, cascadeDelete: true)
                .ForeignKey("dbo.ITEMs", t => t.item_ID, cascadeDelete: true)
                .Index(t => t.Lang_ID)
                .Index(t => t.item_ID);
            
            CreateTable(
                "dbo.Category_lang",
                c => new
                    {
                        category_ID = c.Int(nullable: false),
                        Lang_ID = c.Int(nullable: false),
                        Name = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.category_ID, t.Lang_ID })
                .ForeignKey("dbo.Languages", t => t.Lang_ID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.category_ID, cascadeDelete: true)
                .Index(t => t.Lang_ID)
                .Index(t => t.category_ID);
            
            CreateTable(
                "dbo.MenuItem_lang",
                c => new
                    {
                        Menuitem_ID = c.Int(nullable: false),
                        Lang_ID = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => new { t.Menuitem_ID, t.Lang_ID })
                .ForeignKey("dbo.MenuItems", t => t.Menuitem_ID, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.Lang_ID, cascadeDelete: true)
                .Index(t => t.Menuitem_ID)
                .Index(t => t.Lang_ID);
            
            DropColumn("dbo.ITEMs", "Content");
            DropColumn("dbo.ITEMs", "Image");
            DropColumn("dbo.ITEMs", "Description");
            DropColumn("dbo.Categories", "Image");
            DropColumn("dbo.Categories", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Description", c => c.String());
            AddColumn("dbo.Categories", "Image", c => c.String());
            AddColumn("dbo.ITEMs", "Description", c => c.String());
            AddColumn("dbo.ITEMs", "Image", c => c.String());
            AddColumn("dbo.ITEMs", "Content", c => c.String());
            DropIndex("dbo.MenuItem_lang", new[] { "Lang_ID" });
            DropIndex("dbo.MenuItem_lang", new[] { "Menuitem_ID" });
            DropIndex("dbo.Category_lang", new[] { "category_ID" });
            DropIndex("dbo.Category_lang", new[] { "Lang_ID" });
            DropIndex("dbo.item_lang", new[] { "item_ID" });
            DropIndex("dbo.item_lang", new[] { "Lang_ID" });
            DropForeignKey("dbo.MenuItem_lang", "Lang_ID", "dbo.Languages");
            DropForeignKey("dbo.MenuItem_lang", "Menuitem_ID", "dbo.MenuItems");
            DropForeignKey("dbo.Category_lang", "category_ID", "dbo.Categories");
            DropForeignKey("dbo.Category_lang", "Lang_ID", "dbo.Languages");
            DropForeignKey("dbo.item_lang", "item_ID", "dbo.ITEMs");
            DropForeignKey("dbo.item_lang", "Lang_ID", "dbo.Languages");
            DropTable("dbo.MenuItem_lang");
            DropTable("dbo.Category_lang");
            DropTable("dbo.item_lang");
            DropTable("dbo.Languages");
        }
    }
}
