using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Moedelo.InfrastructureV2.WebApi.Validation.Utils;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class RequiredIfPropEqualsToAttribute : ValidationAttribute
    {
        private readonly string otherPropName;
        private readonly object otherPropRequiredValue;

        public object[] ValidValues { get; set; }

        public RequiredIfPropEqualsToAttribute(string otherPropName, object otherPropRequiredValue, string errorMessage)
            : base(errorMessage)
        {
            this.otherPropName = otherPropName;
            this.otherPropRequiredValue = otherPropRequiredValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            var otherPropVal = type.GetProperty(otherPropName).GetValue(instance, null);
            if (otherPropRequiredValue == null)
            {
                if (otherPropVal != null)
                {
                    return ValidationResult.Success;
                }

                return value == null ? new ValidationResult(ErrorMessage) : CheckIsValidValue(value);
            }

            if (otherPropVal == null)
            {
                return ValidationResult.Success;
            }

            var propType = otherPropRequiredValue.GetType();
            dynamic propValue = Convert.ChangeType(otherPropVal, propType);
            dynamic requiredValue = Convert.ChangeType(otherPropRequiredValue, propType);

            if (requiredValue != propValue)
            {
                return ValidationResult.Success;
            }

            if (propValue == requiredValue)
            {
                if (value == null)
                {
                    return new ValidationResult(ErrorMessage);
                }
            }

            return CheckIsValidValue(value);
        }

        private ValidationResult CheckIsValidValue(object value)
        {
            if (!(ValidValues?.Any() ?? false))
            {
                return ValidationResult.Success;
            }

            // надо проверить на корректность значения
            if (!ValidValues.Any(v => v.MatchValue(value)))
            {
                var errorMessage = $"При {otherPropName}={otherPropRequiredValue.EnumToInt()} поле должно принимать одно из следующих допустимых значений: {string.Join(",", ValidValues.Select(v => v.EnumToInt()))}";
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}