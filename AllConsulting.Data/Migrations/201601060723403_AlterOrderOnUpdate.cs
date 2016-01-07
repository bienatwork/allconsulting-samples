namespace AllConsulting.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterOrderOnUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "OnUpdate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Order", "RowVersion");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
            DropColumn("dbo.Order", "OnUpdate");
        }
    }
}
