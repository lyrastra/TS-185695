using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class GreaterThenAttribute : ValidationAttribute
    {
        private readonly string propName;

        public GreaterThenAttribute(string propName) : base($"Значение должно быть больше, чем указанное в поле {propName}")
        {
            this.propName = propName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            decimal? current = Convert.ToDecimal(value);
            var otherVal = type.GetProperty(propName).GetValue(instance, null);
            if (otherVal == null)
            {
                return ValidationResult.Success;
            }

            var other = Convert.ToDecimal(otherVal);
            if (other >= current)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}