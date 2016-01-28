// Configurations
// *****************************************************************************************
//
// Name:		Configurations.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************
using ACAG.Entities;

namespace ACAG.Data.Configurations
{
    /// <summary>
    /// this is class OrderPositionConfiguration
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
