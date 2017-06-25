namespace VeterinaryClinic.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
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
                    })
                .PrimaryKey(t => t.ProcurementId)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .Index(t => t.ProductId);
            
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
            DropForeignKey("dbo.Procurement", "ProductId", "dbo.Product");
            DropIndex("dbo.Sale", new[] { "ProductId" });
            DropIndex("dbo.Procurement", new[] { "ProductId" });
            DropTable("dbo.Sale");
            DropTable("dbo.Procurement");
        }
    }
}
