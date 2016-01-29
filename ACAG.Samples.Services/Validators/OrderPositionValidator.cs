// ACAG.Samples.BusinessServices.Validators
// *****************************************************************************************
//
// Name:        OrderPositionValidator.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using ACAG.Samples.BusinessServices.Models;
using FluentValidation;

namespace ACAG.Samples.BusinessServices.Validators
{
    public class OrderPositionValidator : AbstractValidator<OrderPositionModel>
    {
        public OrderPositionValidator()
        {
            RuleFor(x => x.PositionNumber).NotEmpty().WithMessage("Order Position is required.");
            RuleFor(x => x.PositionNumber).GreaterThanOrEqualTo(1).WithMessage("Order Position must be greater or equals 1.");
            //RuleFor(x => x.Pieces).NotEmpty().WithMessage("Pieces is required");

            RuleFor(x => x.Text).NotEmpty().WithMessage("Text is required.");

            RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required.");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must equal or be greater than 0.");

            //RuleFor(x => x.Total).NotEmpty().WithMessage("Total is required.");
            //RuleFor(x => x.Total).GreaterThanOrEqualTo(0).WithMessage("Total must equal or be greater than 0");
        }
    }
}
