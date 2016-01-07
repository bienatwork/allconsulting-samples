namespace AllConsulting.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.OrderPosition", "Pieces", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.OrderPosition", "Pieces", c => c.String(nullable: false));
        }
    }
}
