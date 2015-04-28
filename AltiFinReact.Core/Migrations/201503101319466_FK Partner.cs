namespace AltiFinReact.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FKPartner : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Invoice", name: "Partner_Id", newName: "PartnerId");
            RenameIndex(table: "dbo.Invoice", name: "IX_Partner_Id", newName: "IX_PartnerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Invoice", name: "IX_PartnerId", newName: "IX_Partner_Id");
            RenameColumn(table: "dbo.Invoice", name: "PartnerId", newName: "Partner_Id");
        }
    }
}
