// ACAG.Samples.Web.Models
// *****************************************************************************************
//
// Name:        PositionOrderModel.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

namespace ACAG.Samples.Web.Models
{
    public class PositionOrderModel
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