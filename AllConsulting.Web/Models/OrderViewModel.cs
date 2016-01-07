using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AllConsulting.Web.Infrastructure.Validators;

namespace AllConsulting.Web.Models
{
    public class OrderViewModel : IValidatableObject
    {
        public int ID { get; set; }

        public string CustomerNumber { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }

        public double TotalPrice { get; set; }

        public IList<OrderPositionViewModel> OrderPositions { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new OrderViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(x => new ValidationResult(x.ErrorMessage, new[] { x.PropertyName }));
        }
    }
}