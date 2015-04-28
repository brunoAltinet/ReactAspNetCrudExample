namespace AltiFinReact.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceItemAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InvoiceItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(),
                        Ordinal = c.Int(),
                        Name = c.String(maxLength: 500),
                        UnitPrice = c.Decimal(precision: 18, scale: 2),
                        Qty = c.Decimal(precision: 18, scale: 2),
                        Price = c.Decimal(precision: 18, scale: 2),
                        DateCreated = c.DateTime(),
                        CreatedById = c.Int(),
                        DateModified = c.DateTime(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId)
                .Index(t => t.InvoiceId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InvoiceItem", "InvoiceId", "dbo.Invoice");
            DropIndex("dbo.InvoiceItem", new[] { "InvoiceId" });
            DropTable("dbo.InvoiceItem");
        }
    }
}
