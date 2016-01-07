using System;
using System.Collections.Generic; 

namespace AllConsulting.Web.Dto
{
    public class OrderDto
    {
        public int Index { set; get; }
        public int OrderId { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double TotalPrice { get; set; }
        public DateTime? OrderDate { get; set; }

        public List<PositionOrderDto> ListPositionOrderDto { set; get; }
    }
}