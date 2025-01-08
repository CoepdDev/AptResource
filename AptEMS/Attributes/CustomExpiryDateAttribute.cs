using System;
using System.ComponentModel.DataAnnotations;

namespace AptEMS.Attributes
{
    public class CustomExpiryDateAttribute : ValidationAttribute
    {
        private readonly string _fromDatePropertyName;

        // Constructor with optional parameter
        public CustomExpiryDateAttribute(string fromDatePropertyName = null)
        {
            _fromDatePropertyName = fromDatePropertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (_fromDatePropertyName == null)
            {
                // Simple expiry date validation: checks if date is in the future
                if (value is DateTime expiryDate && expiryDate < DateTime.Now)
                {
                    return new ValidationResult("The date must be in the future.");
                }
            }
            else
            {
                // FromDate/ToDate validation
                var fromDateProperty = validationContext.ObjectType.GetProperty(_fromDatePropertyName);
                if (fromDateProperty == null)
                {
                    return new ValidationResult($"Unknown property: {_fromDatePropertyName}");
                }

                var fromDateValue = fromDateProperty.GetValue(validationContext.ObjectInstance, null) as DateTime?;
                var toDateValue = value as DateTime?;

                if (fromDateValue == null || toDateValue == null)
                {
                    return new ValidationResult("Invalid date values provided.");
                }

                if (toDateValue < fromDateValue)
                {
                    return new ValidationResult("The ToDate must be greater than FromDate.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
