// PositionOrderModel
// *****************************************************************************************
//
// Name:		PositionOrderModel.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************
namespace ACAG.Web.Models
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