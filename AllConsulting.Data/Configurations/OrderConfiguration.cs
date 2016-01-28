// Configurations
// *****************************************************************************************
//
// Name:		OrderConfiguration.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************
using ACAG.Entities;

namespace ACAG.Data.Configurations
{
    /// <summary>
    /// Order Configuration
    /// </summary>
    public class OrderConfiguration : EntityBaseConfiguration<Order>
    {
        public OrderConfiguration()
        {
            Property(x => x.CustomerNumber).IsRequired(); 
            Property(x => x.OrderDate).IsOptional();
            Property(x => x.DeliveryDate).IsOptional();
            Property(x => x.TotalPrice).IsOptional();

            HasMany(x => x.OrderPositions).WithRequired(y => y.Order).HasForeignKey(y => y.OrderID);
        }
    }
}
