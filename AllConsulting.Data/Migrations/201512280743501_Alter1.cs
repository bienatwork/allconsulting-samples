namespace AllConsulting.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Order", "CustomerNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Order", "CustomerNumber", c => c.String(nullable: false, maxLength: 500));
        }
    }
}
