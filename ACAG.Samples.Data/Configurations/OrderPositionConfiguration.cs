// ACAG.Samples.Data.Configurations
// *****************************************************************************************
//
// Name:        OrderPositionConfiguration.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using ACAG.Samples.Entities;

namespace ACAG.Samples.Data.Configurations
{
    /// <summary>
    /// Configuration database metadata for fields of OrderPosition entity
    /// </summary>
    public class OrderPositionConfiguration : EntityBaseConfiguration<OrderPosition>
    {
        public OrderPositionConfiguration()
        {
            Property(x => x.PositionNumber).IsRequired();
            //Property(x => x.Pieces).IsRequired();
            Property(x => x.Text).IsRequired();
            Property(x => x.Price).IsRequired();
            Property(x => x.Total).IsRequired();
        }
    }
}
