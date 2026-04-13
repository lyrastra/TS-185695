using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class EnumValueAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var type = value.GetType();

            return !Enum.IsDefined(type, value)
                ? new ValidationResult($"Поле должно принимать одно из следующих допустимых значений: {string.Join(", ", Enum.GetValues(type).Cast<int>())}")
                : ValidationResult.Success;
        }
    }
}