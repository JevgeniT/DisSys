using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Validation
{
    public class AvailabilityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = DateTime.Parse(value.ToString());

            if (date < DateTime.Now)
            {
                return new ValidationResult("Cannot be in the past");
            }

            return ValidationResult.Success;
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value);
        }
    }
}