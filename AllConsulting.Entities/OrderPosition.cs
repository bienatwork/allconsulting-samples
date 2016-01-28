namespace ACAG.Entities
{
    /// <summary>
    /// Represents a OrderPosition
    /// </summary>
    public class OrderPosition : IEntityBase
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the OrderID
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// Gets or sets the PositionNumber
        /// </summary>
        public int PositionNumber { get; set; }
        /// <summary>
        /// Gets or sets the Pieces
        /// </summary>
        public string Pieces { get; set; }
        /// <summary>
        /// Gets or sets the Text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Gets or sets the Total price
        /// </summary>
        public double Total { get; set; }
        /// <summary>
        /// Gets or sets the Order
        /// </summary>
        public Order Order { get; set; }
    }
}
