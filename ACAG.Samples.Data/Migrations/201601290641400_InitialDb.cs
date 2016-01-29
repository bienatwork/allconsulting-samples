namespace ACAG.Samples.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Error",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            Message = c.String(),
            //            StackTrace = c.String(),
            //            DateCreated = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.OrderPosition",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            OrderID = c.Int(nullable: false),
            //            PositionNumber = c.Int(nullable: false),
            //            Pieces = c.String(),
            //            Text = c.String(nullable: false),
            //            Price = c.Double(nullable: false),
            //            Total = c.Double(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.Order", t => t.OrderID, cascadeDelete: true)
            //    .Index(t => t.OrderID);
            
            //CreateTable(
            //    "dbo.Order",
            //    c => new
            //        {
            //            ID = c.Int(nullable: false, identity: true),
            //            CustomerNumber = c.String(nullable: false),
            //            OrderDate = c.DateTime(),
            //            DeliveryDate = c.DateTime(),
            //            TotalPrice = c.Double(),
            //            OnUpdate = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderPosition", "OrderID", "dbo.Order");
            DropIndex("dbo.OrderPosition", new[] { "OrderID" });
            DropTable("dbo.Order");
            DropTable("dbo.OrderPosition");
            DropTable("dbo.Error");
        }
    }
}
