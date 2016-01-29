// ACAG.Samples.Entities
// *****************************************************************************************
//
// Name:        Order.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System;
using System.Collections.Generic;

namespace ACAG.Samples.Entities
{
    /// <summary>
    /// Represents an order
    /// </summary>
    public class Order : IEntityBase
    {
        public Order()
        {
            OrderPositions = new List<OrderPosition>();
            OnUpdate = DateTime.Now;
        }
        
        public int ID { get; set; }

        /// <summary>
        /// Number of the customer
        /// </summary>
        public string CustomerNumber { get; set; }
        
        /// <summary>
        /// The time the order is placed
        /// </summary>
        public DateTime? OrderDate { get; set; }
        
        /// <summary>
        /// The time the order could be deliveried to customer
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        
        /// <summary>
        /// Total price of the order
        /// </summary>
        public double TotalPrice { get; set; }
        
        /// <summary>
        /// The time the order might be changed
        /// </summary>
        public DateTime OnUpdate { get; set; }
        
        /// <summary>
        /// Collection of Position the order consist of
        /// </summary>
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
    }
}
