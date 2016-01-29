// ACAG.Samples.BusinessServices.Models
// *****************************************************************************************
//
// Name:        SearchResultModel.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ACAG.Samples.BusinessServices.Models
{
    [DataContract]
   public class SearchResultModel
    {
        [DataMember]
        public List<OrderModel> ListOrder { set; get; }
        [DataMember]
        public int TotalRows { set; get; } 
    }
}
