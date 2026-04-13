using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class NotGreaterThenAttribute : ValidationAttribute
    {
        private readonly string propName;

        public NotGreaterThenAttribute(string propName) : base($"Значение не должно превышать значение, указанное в поле {propName}")
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

            return otherVal != null && current <= Convert.ToDecimal(otherVal)
                ? ValidationResult.Success
                : new ValidationResult(this.FormatErrorMessage(context.DisplayName), context.MemberName != null ? new [] { context.MemberName } : null);
        }
    }
}