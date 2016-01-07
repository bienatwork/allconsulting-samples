using System;
using System.Collections.Generic; 

namespace AllConsulting.Entities
{
    public class Order : IEntityBase
    {
        public Order()
        {
            OrderPositions = new List<OrderPosition>();
            OnUpdate = DateTime.Now;
        }

        public int ID { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OnUpdate { get; set; }

        public virtual ICollection<OrderPosition> OrderPositions { get; set; }
    }
}
