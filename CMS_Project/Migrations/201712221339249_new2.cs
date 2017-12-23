namespace CMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fields", "ItemLangId", "dbo.item_lang");
            DropForeignKey("dbo.Fields", "item_lang_ID", "dbo.item_lang");
            DropIndex("dbo.Fields", new[] { "ItemLangId" });
            DropIndex("dbo.Fields", new[] { "item_lang_ID" });
            AlterColumn("dbo.Fields", "ItemLangId", c => c.Int(nullable: false));
            AddForeignKey("dbo.Fields", "ItemLangId", "dbo.item_lang", "ID", cascadeDelete: true);
            CreateIndex("dbo.Fields", "ItemLangId");
            DropColumn("dbo.Fields", "item_lang_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fields", "item_lang_ID", c => c.Int());
            DropIndex("dbo.Fields", new[] { "ItemLangId" });
            DropForeignKey("dbo.Fields", "ItemLangId", "dbo.item_lang");
            AlterColumn("dbo.Fields", "ItemLangId", c => c.Int());
            CreateIndex("dbo.Fields", "item_lang_ID");
            CreateIndex("dbo.Fields", "ItemLangId");
            AddForeignKey("dbo.Fields", "item_lang_ID", "dbo.item_lang", "ID");
            AddForeignKey("dbo.Fields", "ItemLangId", "dbo.item_lang", "ID");
        }
    }
}
