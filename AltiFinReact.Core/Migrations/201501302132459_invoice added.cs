namespace AltiFinReact.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoiceadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(nullable: false),
                        CreatedById = c.Int(),
                        DateModified = c.DateTime(),
                        ModifiedById = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Invoice");
        }
    }
}
