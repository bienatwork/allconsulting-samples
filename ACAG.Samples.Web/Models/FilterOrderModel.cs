// ACAG.Samples.Web.Models
// *****************************************************************************************
//
// Name:        FilterOrderModel.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System.Collections.Generic;

namespace ACAG.Samples.Web.Models
{
    public class FilterOrderModel
    {
        public List<OrderModel> ListOrder { set; get; }
        public int TotalRows { set; get; } 
    }
}