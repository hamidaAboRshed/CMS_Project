namespace CMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MenuItems", "Parent_ID", "dbo.MenuItems");
            DropIndex("dbo.MenuItems", new[] { "Parent_ID" });
            CreateTable(
                "dbo.ITEMs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Image = c.String(),
                        Description = c.String(),
                        Cat_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Cat_ID, cascadeDelete: true)
                .Index(t => t.Cat_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        username = c.String(),
                        password = c.String(),
                        fullname = c.String(),
                        email = c.String(),
                        active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.MenuItems", "ItemId", c => c.Int());
            AddColumn("dbo.MenuItems", "CatId", c => c.Int(nullable: false));
            AlterColumn("dbo.MenuItems", "Parent_Id", c => c.Int());
            AddForeignKey("dbo.MenuItems", "Parent_Id", "dbo.MenuItems", "ID");
            AddForeignKey("dbo.MenuItems", "ItemId", "dbo.ITEMs", "ID");
            CreateIndex("dbo.MenuItems", "Parent_Id");
            CreateIndex("dbo.MenuItems", "ItemId");
            DropColumn("dbo.MenuItems", "Name");
            DropColumn("dbo.Categories", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Name", c => c.String());
            AddColumn("dbo.MenuItems", "Name", c => c.String());
            DropIndex("dbo.ITEMs", new[] { "Cat_ID" });
            DropIndex("dbo.MenuItems", new[] { "ItemId" });
            DropIndex("dbo.MenuItems", new[] { "Parent_Id" });
            DropForeignKey("dbo.ITEMs", "Cat_ID", "dbo.Categories");
            DropForeignKey("dbo.MenuItems", "ItemId", "dbo.ITEMs");
            DropForeignKey("dbo.MenuItems", "Parent_Id", "dbo.MenuItems");
            AlterColumn("dbo.MenuItems", "Parent_ID", c => c.Int());
            DropColumn("dbo.MenuItems", "CatId");
            DropColumn("dbo.MenuItems", "ItemId");
            DropTable("dbo.Users");
            DropTable("dbo.ITEMs");
            CreateIndex("dbo.MenuItems", "Parent_ID");
            AddForeignKey("dbo.MenuItems", "Parent_ID", "dbo.MenuItems", "ID");
        }
    }
}
