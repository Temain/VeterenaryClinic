namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Procurement", "Product_ProductId", "dbo.Product");
            DropForeignKey("dbo.Procurement", "Product_ProductId1", "dbo.Product");
            DropForeignKey("dbo.Procurement", "Product_ProductId2", "dbo.Product");
            DropForeignKey("dbo.Sale", "ProductId", "dbo.Product");
            DropIndex("dbo.Procurement", new[] { "Product_ProductId" });
            DropIndex("dbo.Procurement", new[] { "Product_ProductId1" });
            DropIndex("dbo.Procurement", new[] { "Product_ProductId2" });
            DropIndex("dbo.Sale", new[] { "ProductId" });
            DropTable("dbo.Procurement");
            DropTable("dbo.Sale");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sale",
                c => new
                    {
                        SaleId = c.Int(nullable: false, identity: true),
                        SaleDate = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SaleCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SaleId);
            
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
                .PrimaryKey(t => t.ProcurementId);
            
            CreateIndex("dbo.Sale", "ProductId");
            CreateIndex("dbo.Procurement", "Product_ProductId2");
            CreateIndex("dbo.Procurement", "Product_ProductId1");
            CreateIndex("dbo.Procurement", "Product_ProductId");
            AddForeignKey("dbo.Sale", "ProductId", "dbo.Product", "ProductId");
            AddForeignKey("dbo.Procurement", "Product_ProductId2", "dbo.Product", "ProductId");
            AddForeignKey("dbo.Procurement", "Product_ProductId1", "dbo.Product", "ProductId");
            AddForeignKey("dbo.Procurement", "Product_ProductId", "dbo.Product", "ProductId");
        }
    }
}
