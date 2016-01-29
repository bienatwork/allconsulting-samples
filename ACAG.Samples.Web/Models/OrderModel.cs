// ACAG.Samples.Web.Models
// *****************************************************************************************
//
// Name:        OrderModel.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System;
using System.Collections.Generic;

namespace ACAG.Samples.Web.Models
{
    public class OrderModel
    {
        public int Index { set; get; }
        public int OrderId { get; set; }
        public string CustomerNumber { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public double TotalPrice { get; set; }
        public DateTime? OrderDate { get; set; }

        public List<PositionOrderModel> ListPositionOrderDto { set; get; } 
    }
}