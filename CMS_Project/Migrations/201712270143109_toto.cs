namespace CMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MenuItems", "Template_ID", c => c.Int());
            AlterColumn("dbo.MenuItems", "Template_Id", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MenuItems", "Template_ID", c => c.Int());
            AlterColumn("dbo.MenuItems", "Template_Id", c => c.Int());
        }
    }
}
