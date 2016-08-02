namespace ShopListAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ShopItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        Count = c.Int(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        Rating = c.Int(nullable: false),
                        CheckedOut = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShopItems");
        }
    }
}
