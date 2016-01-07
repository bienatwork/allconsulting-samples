namespace AllConsulting.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteredOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "OrderDate", c => c.DateTime());
            DropColumn("dbo.Order", "OrderDated");
             
        }
        
        public override void Down()
        {
            AddColumn("dbo.Order", "OrderDated", c => c.DateTime());
            DropColumn("dbo.Order", "OrderDate"); 
        }
    }
}
