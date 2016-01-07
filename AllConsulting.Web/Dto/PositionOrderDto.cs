 
namespace AllConsulting.Web.Dto
{
    public class PositionOrderDto
    {
        private double _total = 0;
        public int PositionOrderId { get; set; }
        public int OrderId { get; set; }
        public int PositionNumber { get; set; }
        public string Pieces { get; set; }
        public string Text { get; set; }
        public double Price { get; set; }
        public double Total
        {
            get { return PositionNumber * Price; }
            set { _total = value; }
        } 
    }
}