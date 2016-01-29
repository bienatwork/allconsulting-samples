// ACAG.Samples.BusinessServices.Models
// *****************************************************************************************
//
// Name:        OrderPositionModel.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System.Runtime.Serialization;
using ACAG.Samples.BusinessServices.Validators;
using ACAG.Samples.CommonLib;
using FluentValidation;

namespace ACAG.Samples.BusinessServices.Models
{
    [DataContract]
    public class OrderPositionModel : Validatable,IExtensibleDataObject
    {
        private double _total;

        [DataMember]
        public int PositionOrderId { get; set; }

        [DataMember]
        public int OrderId { get; set; }

        [DataMember]
        public int PositionNumber { get; set; }

        [DataMember]
        public string Pieces { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public double Total
        {
            get { return PositionNumber * Price; }
            set { _total = value; }
        }

        public ExtensionDataObject ExtensionData { get; set; }

        protected override IValidator GetValidator()
        {
            return new OrderPositionValidator();
        }
    }
}
