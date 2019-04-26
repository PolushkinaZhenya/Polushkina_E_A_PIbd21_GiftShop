namespace GiftShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Procedures",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CustomerId = c.Int(nullable: false),
                    SetId = c.Int(nullable: false),
                    Count = c.Int(nullable: false),
                    Sum = c.Decimal(nullable: false, precision: 18, scale:2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Sets", t => t.SetId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.SetId);
            
            CreateTable(
                "dbo.Sets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SetName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PartName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SetParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SetId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                        PartName = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parts", t => t.PartId, cascadeDelete: true)
                .Index(t => t.PartId);
            
            CreateTable(
                "dbo.StorageParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Parts", t => t.PartId, cascadeDelete: true)
                .ForeignKey("dbo.Storages", t => t.StorageId, cascadeDelete: true)
                .Index(t => t.StorageId)
                .Index(t => t.PartId);
            
            CreateTable(
                "dbo.Storages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StorageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StorageParts", "StorageId", "dbo.Storages");
            DropForeignKey("dbo.StorageParts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.SetParts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.Procedures", "SetId", "dbo.Sets");
            DropForeignKey("dbo.Procedures", "CustomerId", "dbo.Customers");
            DropIndex("dbo.StorageParts", new[] { "PartId" });
            DropIndex("dbo.StorageParts", new[] { "StorageId" });
            DropIndex("dbo.SetParts", new[] { "PartId" });
            DropIndex("dbo.Procedures", new[] { "SetId" });
            DropIndex("dbo.Procedures", new[] { "CustomerId" });
            DropTable("dbo.Storages");
            DropTable("dbo.StorageParts");
            DropTable("dbo.SetParts");
            DropTable("dbo.Parts");
            DropTable("dbo.Sets");
            DropTable("dbo.Procedures");
            DropTable("dbo.Customers");
        }
    }
}
