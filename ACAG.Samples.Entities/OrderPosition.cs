// ACAG.Samples.Entities
// *****************************************************************************************
//
// Name:        OrderPosition.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

namespace ACAG.Samples.Entities
{
    /// <summary>
    /// Represents an OrderPosition
    /// </summary>
    public class OrderPosition : IEntityBase
    {
        public int ID { get; set; }

        /// <summary>
        /// The identifier of order who this position belong to. It is as foreign key in the relationship
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// The number of position
        /// </summary>
        public int PositionNumber { get; set; }

        /// <summary>
        /// The pieces about the poisition
        /// </summary>
        public string Pieces { get; set; }

        /// <summary>
        /// Any description about the position
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The unit price of the position
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The sub total of position
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// The order who take relationship to this position
        /// </summary>
        public Order Order { get; set; }
    }
}
