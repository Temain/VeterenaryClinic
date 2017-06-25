namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PetShop : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Procurement",
                c => new
                    {
                        ProcurementId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ProcurementDate = c.DateTime(nullable: false),
                        ProcurementAmount = c.Int(nullable: false),
                        Product_ProductId = c.Int(),
                        Product_ProductId1 = c.Int(),
                        Product_ProductId2 = c.Int(),
                    })
                .PrimaryKey(t => t.ProcurementId)
                .ForeignKey("dbo.Product", t => t.Product_ProductId)
                .ForeignKey("dbo.Product", t => t.Product_ProductId1)
                .ForeignKey("dbo.Product", t => t.Product_ProductId2)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Product_ProductId1)
                .Index(t => t.Product_ProductId2);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductName = c.String(),
                        PurchasePrice = c.Int(nullable: false),
                        SellingPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        SaleDate = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SaleCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sale", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Procurement", "Product_ProductId2", "dbo.Product");
            DropForeignKey("dbo.Procurement", "Product_ProductId1", "dbo.Product");
            DropForeignKey("dbo.Procurement", "Product_ProductId", "dbo.Product");
            DropIndex("dbo.Sale", new[] { "ProductId" });
            DropIndex("dbo.Procurement", new[] { "Product_ProductId2" });
            DropIndex("dbo.Procurement", new[] { "Product_ProductId1" });
            DropIndex("dbo.Procurement", new[] { "Product_ProductId" });
            DropTable("dbo.Sale");
            DropTable("dbo.Product");
            DropTable("dbo.Procurement");
        }
    }
}
