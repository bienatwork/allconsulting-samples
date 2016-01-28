// FilterOrderModel
// *****************************************************************************************
//
// Name:		FilterOrderModel.cs
//
// Created:		28.01.2016 ACAG  
// Modified:	28.01.2016 ACAG  	: Creation 
//
// *****************************************************************************************
using System.Collections.Generic;

namespace ACAG.Web.Models
{
    public class FilterOrderModel
    {
        public List<OrderModel> ListOrder { set; get; }
        public int TotalRows { set; get; } 
    }
}