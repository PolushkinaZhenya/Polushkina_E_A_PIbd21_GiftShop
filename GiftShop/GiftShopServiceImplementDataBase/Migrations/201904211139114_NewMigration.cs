namespace GiftShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SellerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Procedures", "SellerId", c => c.Int());
            CreateIndex("dbo.Procedures", "SellerId");
            AddForeignKey("dbo.Procedures", "SellerId", "dbo.Sellers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Procedures", "SellerId", "dbo.Sellers");
            DropIndex("dbo.Procedures", new[] { "SellerId" });
            DropColumn("dbo.Procedures", "SellerId");
            DropTable("dbo.Sellers");
        }
    }
}
