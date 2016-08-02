namespace ShopListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_in_shopItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopItems", "ModifidedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ShopItems", "ModifidedBy");
        }
    }
}
