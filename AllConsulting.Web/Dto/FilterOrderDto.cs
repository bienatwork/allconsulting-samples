 using System.Collections.Generic;

namespace AllConsulting.Web.Dto
{
    public class FilterOrderDto
    {
        public List<OrderDto> ListOrder { set; get; }
        public int TotalRows { set; get; }
    }
}