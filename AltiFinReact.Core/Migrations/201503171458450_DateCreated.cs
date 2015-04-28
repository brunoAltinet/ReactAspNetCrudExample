namespace AltiFinReact.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateCreated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Invoice", "DateCreated", c => c.DateTime(defaultValueSql: "GETDATE()"));
            AlterColumn("dbo.Partner", "DateCreated", c => c.DateTime(defaultValueSql: "GETDATE()"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Partner", "DateCreated", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Invoice", "DateCreated", c => c.DateTime(nullable: false));
        }
    }
}
