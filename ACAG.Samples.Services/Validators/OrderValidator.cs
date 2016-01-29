// ACAG.Samples.BusinessServices.Validators
// *****************************************************************************************
//
// Name:        OrderValidator.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System;
using ACAG.Samples.BusinessServices.Models;
using FluentValidation;

namespace ACAG.Samples.BusinessServices.Validators
{
    public class OrderValidator : AbstractValidator<OrderModel>
    {
        public OrderValidator()
        {
            RuleFor(x => x.CustomerNumber).NotEmpty().WithMessage("Customer number is required.");
            RuleFor(x => x.CustomerNumber)
                .Length(1, 20)
                .WithMessage("Customer number must not greater than 20 characters");

            //RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Order date is required");
            //RuleFor(x => x.OrderDate).Must(EqualOrGreaterThanToday).WithMessage("Order date must be equal or greater than today");

            //RuleFor(x => x.DeliveryDate).NotEmpty().WithMessage("Delivery date is required");
            RuleFor(x => x.DeliveryDate).Must(EqualOrGreaterThanToday).WithMessage("Delivery date must be equal or greater than today");
            //RuleFor(x => x.DeliveryDate).GreaterThanOrEqualTo(x => x.OrderDate).WithMessage("Delivery date must be equal or greater than Order date");

            //RuleFor(x => x.TotalPrice).NotEmpty().WithMessage("Total price is required");
            //RuleFor(x => x.TotalPrice).GreaterThanOrEqualTo(0).WithMessage("Total price must greater than 0");
        }

        private bool EqualOrGreaterThanToday(DateTime? arg)
        {
            if (!arg.HasValue)
                return false;

            DateTime argVal = arg.Value;
            return argVal.Date.CompareTo(DateTime.Today.Date) == 0 || argVal.Date.CompareTo(DateTime.Today.Date) > 0;
        }
    }
}
