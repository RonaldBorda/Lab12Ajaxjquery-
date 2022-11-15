namespace CodeFirstDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Invoce : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ProductInvoices", newName: "InvoiceProducts");
            DropPrimaryKey("dbo.InvoiceProducts");
            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                    {
                        InvoiceDetailID = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        InvoiceNumber = c.String(),
                        CustomerID = c.Int(nullable: false),
                        SellerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceDetailID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.SellerID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.SellerID);
            
            AddColumn("dbo.Products", "InvoiceDetail_InvoiceDetailID", c => c.Int());
            AddPrimaryKey("dbo.InvoiceProducts", new[] { "Invoice_InvoiceID", "Product_ProductID" });
            CreateIndex("dbo.Products", "InvoiceDetail_InvoiceDetailID");
            AddForeignKey("dbo.Products", "InvoiceDetail_InvoiceDetailID", "dbo.InvoiceDetails", "InvoiceDetailID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceDetails", "SellerID", "dbo.Sellers");
            DropForeignKey("dbo.Products", "InvoiceDetail_InvoiceDetailID", "dbo.InvoiceDetails");
            DropForeignKey("dbo.InvoiceDetails", "CustomerID", "dbo.Customers");
            DropIndex("dbo.Products", new[] { "InvoiceDetail_InvoiceDetailID" });
            DropIndex("dbo.InvoiceDetails", new[] { "SellerID" });
            DropIndex("dbo.InvoiceDetails", new[] { "CustomerID" });
            DropPrimaryKey("dbo.InvoiceProducts");
            DropColumn("dbo.Products", "InvoiceDetail_InvoiceDetailID");
            DropTable("dbo.InvoiceDetails");
            AddPrimaryKey("dbo.InvoiceProducts", new[] { "Product_ProductID", "Invoice_InvoiceID" });
            RenameTable(name: "dbo.InvoiceProducts", newName: "ProductInvoices");
        }
    }
}
