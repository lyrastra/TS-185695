using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class RequiredNullIfPropEqualsToAttribute : ValidationAttribute
    {
        private readonly string otherPropName;
        private readonly object otherPropRequiredValue;

        public RequiredNullIfPropEqualsToAttribute(
            string otherPropName,
            object otherPropRequiredValue,
            string errorMessage)
            : base(errorMessage)
        {
            this.otherPropName = otherPropName;
            this.otherPropRequiredValue = otherPropRequiredValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            var otherPropValue = type.GetProperty(otherPropName).GetValue(instance, null);
            if (otherPropRequiredValue == null)
            {
                return otherPropValue == null ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
            }

            var propType = otherPropRequiredValue.GetType();

            dynamic propValue = Convert.ChangeType(otherPropValue, propType);
            dynamic requiredValue = Convert.ChangeType(otherPropRequiredValue, propType);

            return requiredValue != propValue ? ValidationResult.Success : new ValidationResult(ErrorMessage);
        }
    }
}
