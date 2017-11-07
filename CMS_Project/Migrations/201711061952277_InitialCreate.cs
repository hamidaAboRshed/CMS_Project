namespace CMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Visible = c.Boolean(nullable: false),
                        Parent_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MenuItems", t => t.Parent_ID)
                .Index(t => t.Parent_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.MenuItems", new[] { "Parent_ID" });
            DropForeignKey("dbo.MenuItems", "Parent_ID", "dbo.MenuItems");
            DropTable("dbo.MenuItems");
        }
    }
}
