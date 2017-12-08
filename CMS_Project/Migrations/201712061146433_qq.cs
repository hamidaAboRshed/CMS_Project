namespace CMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class qq : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Custom_Cat", "Cat_ID", "dbo.Category_lang");
            DropForeignKey("dbo.Custom_Cat", "Field_ID", "dbo.Fields");
            DropForeignKey("dbo.Custom_Item", "ItemID", "dbo.item_lang");
            DropForeignKey("dbo.Roles", "User_ID", "dbo.Users");
            DropIndex("dbo.Custom_Cat", new[] { "Cat_ID" });
            DropIndex("dbo.Custom_Cat", new[] { "Field_ID" });
            DropIndex("dbo.Custom_Item", new[] { "ItemID" });
            DropIndex("dbo.Roles", new[] { "User_ID" });
            CreateTable(
                "dbo.Customs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Cust_Name = c.String(),
                        Cust_Type = c.String(),
                        Cat_ID = c.Int(),
                        Field_ID = c.Int(),
                        Requierd = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Category_lang", t => t.Cat_ID)
                .ForeignKey("dbo.Fields", t => t.Field_ID)
                .Index(t => t.Cat_ID)
                .Index(t => t.Field_ID);
            
            AddColumn("dbo.Users", "Role_ID", c => c.Int());
            AddForeignKey("dbo.Users", "Role_ID", "dbo.Roles", "ID");
            CreateIndex("dbo.Users", "Role_ID");
            DropColumn("dbo.Roles", "User_ID");
            DropTable("dbo.Custom_Cat");
            DropTable("dbo.Custom_Item");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Custom_Item",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ItemID = c.Int(),
                        Field_Key = c.String(),
                        Field_Val = c.String(),
                        Field_Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Custom_Cat",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Cust_Name = c.String(),
                        Cust_Type = c.String(),
                        Cat_ID = c.Int(),
                        Field_ID = c.Int(),
                        Requierd = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Roles", "User_ID", c => c.Int());
            DropIndex("dbo.Customs", new[] { "Field_ID" });
            DropIndex("dbo.Customs", new[] { "Cat_ID" });
            DropIndex("dbo.Users", new[] { "Role_ID" });
            DropForeignKey("dbo.Customs", "Field_ID", "dbo.Fields");
            DropForeignKey("dbo.Customs", "Cat_ID", "dbo.Category_lang");
            DropForeignKey("dbo.Users", "Role_ID", "dbo.Roles");
            DropColumn("dbo.Users", "Role_ID");
            DropTable("dbo.Customs");
            CreateIndex("dbo.Roles", "User_ID");
            CreateIndex("dbo.Custom_Item", "ItemID");
            CreateIndex("dbo.Custom_Cat", "Field_ID");
            CreateIndex("dbo.Custom_Cat", "Cat_ID");
            AddForeignKey("dbo.Roles", "User_ID", "dbo.Users", "ID");
            AddForeignKey("dbo.Custom_Item", "ItemID", "dbo.item_lang", "ID");
            AddForeignKey("dbo.Custom_Cat", "Field_ID", "dbo.Fields", "FieldID");
            AddForeignKey("dbo.Custom_Cat", "Cat_ID", "dbo.Category_lang", "ID");
        }
    }
}
