// ACAG.Samples.Data.Configurations
// *****************************************************************************************
//
// Name:        OrderConfiguration.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using ACAG.Samples.Entities;

namespace ACAG.Samples.Data.Configurations
{
    /// <summary>
    /// Configuration database metadata for fields of Order entity
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
