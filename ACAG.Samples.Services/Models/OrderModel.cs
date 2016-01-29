// ACAG.Samples.BusinessServices.Models
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
using System.Runtime.Serialization;
using ACAG.Samples.BusinessServices.Validators;
using ACAG.Samples.CommonLib;
using FluentValidation;

namespace ACAG.Samples.BusinessServices.Models
{
    [DataContract]
    public class OrderModel :Validatable, IExtensibleDataObject
    {
        [DataMember]
        public int Index { set; get; }

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public string CustomerNumber { get; set; }

        [DataMember]
        public DateTime? DeliveryDate { get; set; }

        [DataMember]
        public double TotalPrice { get; set; }

        [DataMember]
        public DateTime? OrderDate { get; set; }

        [DataMember]
        public List<OrderPositionModel> OrderPositions { set; get; }

        public ExtensionDataObject ExtensionData { get; set; }

        protected override IValidator GetValidator()
        {
            return new OrderValidator();
        }
    }
}
