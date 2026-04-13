using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class LessThenAttribute : ValidationAttribute
    {
        private readonly string propName;

        public LessThenAttribute(string propName)
        {
            this.propName = propName;
            ErrorMessage = $"Значение должно быть меньше, чем указанное в поле {propName}";
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
            if (current >= other)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}