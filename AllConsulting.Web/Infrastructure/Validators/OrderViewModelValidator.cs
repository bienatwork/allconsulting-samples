
using System;
using AllConsulting.Web.Models;
using FluentValidation;

namespace AllConsulting.Web.Infrastructure.Validators
{
    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {
            RuleFor(x => x.CustomerNumber).NotEmpty().WithMessage("Customer number is required.");
            RuleFor(x => x.CustomerNumber)
                .Length(1, 5)
                .WithMessage("Customer number must not greater than 5 characters");

            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Order date is required");
            RuleFor(x => x.OrderDate).Must(EqualOrGreaterThanToday).WithMessage("Order date must be equal or greater than today");

            RuleFor(x => x.DeliveryDate).NotEmpty().WithMessage("Delivery date is required");
            RuleFor(x => x.DeliveryDate).Must(EqualOrGreaterThanToday).WithMessage("Delivery date must be equal or greater than today");
            RuleFor(x => x.DeliveryDate).GreaterThanOrEqualTo(x => x.OrderDate).WithMessage("Delivery date must be equal or greater than Order date");

            RuleFor(x => x.TotalPrice).NotEmpty().WithMessage("Total price is required");
            RuleFor(x => x.TotalPrice).GreaterThanOrEqualTo(0).WithMessage("Total price must greater than zero");
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