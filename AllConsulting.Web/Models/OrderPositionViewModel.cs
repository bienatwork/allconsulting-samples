namespace AllConsulting.Web.Models
{
    public class OrderPositionViewModel
    {
        public int ID { get; set; }
        public int PositionNumber { get; set; }
        public int Pieces { get; set; }
        public string Text { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public int OrderID { get; set; }
    }
}