namespace AllConsulting.Entities
{
    public class OrderPosition : IEntityBase
    {
        public int ID { get; set; }
        public int PositionNumber { get; set; }
        public string Pieces { get; set; }
        public string Text { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }


        public int OrderID { get; set; }
        public Order Order { get; set; }
    }
}
