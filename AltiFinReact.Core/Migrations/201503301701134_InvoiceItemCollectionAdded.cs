namespace AltiFinReact.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceItemCollectionAdded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoiceItem", "InvoiceId", "dbo.Invoice");
            DropIndex("dbo.InvoiceItem", new[] { "InvoiceId" });
            AlterColumn("dbo.InvoiceItem", "InvoiceId", c => c.Int(nullable: false));
            CreateIndex("dbo.InvoiceItem", "InvoiceId");
            AddForeignKey("dbo.InvoiceItem", "InvoiceId", "dbo.Invoice", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItem", "InvoiceId", "dbo.Invoice");
            DropIndex("dbo.InvoiceItem", new[] { "InvoiceId" });
            AlterColumn("dbo.InvoiceItem", "InvoiceId", c => c.Int());
            CreateIndex("dbo.InvoiceItem", "InvoiceId");
            AddForeignKey("dbo.InvoiceItem", "InvoiceId", "dbo.Invoice", "Id");
        }
    }
}
