namespace AltiFinReact.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPartner : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Partner",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 500),
                        PhoneNumber = c.String(maxLength: 50),
                        Address = c.String(maxLength: 500),
                        Oib = c.String(maxLength: 50),
                        ContactPerson = c.String(maxLength: 100),
                        AccountNumber = c.String(maxLength: 50),
                        City = c.String(maxLength: 50),
                        Country = c.String(maxLength: 100),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        DateModified = c.DateTime(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Invoice", "Ordinal", c => c.Int());
            AddColumn("dbo.Invoice", "OrderNumber", c => c.String(maxLength: 100));
            AddColumn("dbo.Invoice", "Date", c => c.DateTime());
            AddColumn("dbo.Invoice", "NettoValue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Invoice", "TaxValue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Invoice", "BruttoValue", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Invoice", "DiscountPct", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Invoice", "PaymentDeadline", c => c.DateTime());
            AddColumn("dbo.Invoice", "Note", c => c.String(maxLength: 500));
            AddColumn("dbo.Invoice", "Partner_Id", c => c.Int());
            AlterColumn("dbo.Invoice", "Code", c => c.String(maxLength: 100));
            CreateIndex("dbo.Invoice", "Partner_Id");
            AddForeignKey("dbo.Invoice", "Partner_Id", "dbo.Partner", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Invoice", "Partner_Id", "dbo.Partner");
            DropIndex("dbo.Invoice", new[] { "Partner_Id" });
            AlterColumn("dbo.Invoice", "Code", c => c.String());
            DropColumn("dbo.Invoice", "Partner_Id");
            DropColumn("dbo.Invoice", "Note");
            DropColumn("dbo.Invoice", "PaymentDeadline");
            DropColumn("dbo.Invoice", "DiscountPct");
            DropColumn("dbo.Invoice", "BruttoValue");
            DropColumn("dbo.Invoice", "TaxValue");
            DropColumn("dbo.Invoice", "NettoValue");
            DropColumn("dbo.Invoice", "Date");
            DropColumn("dbo.Invoice", "OrderNumber");
            DropColumn("dbo.Invoice", "Ordinal");
            DropTable("dbo.Partner");
        }
    }
}
