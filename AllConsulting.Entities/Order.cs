using System;
using System.Collections.Generic; 

namespace ACAG.Entities
{
    /// <summary>
    /// Represents a Order
    /// </summary>
    public class Order : IEntityBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Order()
        {
            OrderPositions = new List<OrderPosition>();
            OnUpdate = DateTime.Now;
        }
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the CustomerNumber
        /// </summary>
        public string CustomerNumber { get; set; }
        /// <summary>
        /// Gets or sets the OrderDate
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// Gets or sets the DeliveryDate
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// Gets or sets the TotalPrice
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// Gets or sets the OnUpdate
        /// </summary>
        public DateTime OnUpdate { get; set; }
        /// <summary>
        /// Gets or sets the list position
        /// </summary>
        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
    }
}
