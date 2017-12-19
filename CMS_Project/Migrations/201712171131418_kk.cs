namespace CMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kk : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Category_lang", "temp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Category_lang", "temp", c => c.Int(nullable: false));
        }
    }
}
