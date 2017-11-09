namespace CMS_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itm2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ITEMs", "Image", c => c.String());
            AddColumn("dbo.ITEMs", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ITEMs", "Description");
            DropColumn("dbo.ITEMs", "Image");
        }
    }
}
