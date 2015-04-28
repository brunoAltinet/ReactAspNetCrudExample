namespace AltiFinReact.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoicecode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoice", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoice", "Code");
        }
    }
}
