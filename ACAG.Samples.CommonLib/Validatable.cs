// ACAG.Samples.CommonLib
// *****************************************************************************************
//
// Name:        Validatable.cs
//
// Created:     28.01.2016 Kloon  
// Modified:    28.01.2016 Kloon    : Creation 
//
// *****************************************************************************************

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace ACAG.Samples.CommonLib

{
    [DataContract]
    public class Validatable
    {
        protected IValidator _validator = null;
        protected IEnumerable<ValidationFailure> _validityErrors = null;

        protected virtual IValidator GetValidator()
        {
            return null;
        }

        public IEnumerable<ValidationFailure> ValidityErrors
        {
            get { return _validityErrors; }
            set { }
        }

        public void Validate()
        {
            if (_validator != null)
            {
                ValidationResult results = _validator.Validate(this);
                _validityErrors = results.Errors;
            }
        }

        public virtual bool IsValid
        {
            get
            {
                if (_validityErrors != null && _validityErrors.Count() > 0)
                    return false;
                else
                    return true;
            }
        }
    }
}
