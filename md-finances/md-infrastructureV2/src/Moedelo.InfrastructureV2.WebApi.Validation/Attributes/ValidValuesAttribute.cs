using System.ComponentModel.DataAnnotations;
using System.Linq;
using Moedelo.InfrastructureV2.WebApi.Validation.Utils;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class ValidValuesAttribute : ValidationAttribute
    {
        private readonly object[] allowedValues;

        public bool AllowNull { get; set; }

        public ValidValuesAttribute(params object[] allowedValues)
        {
            this.allowedValues = allowedValues;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null && AllowNull)
            {
                return ValidationResult.Success;
            }

            var success = allowedValues.Any(x => x.MatchValue(value));
            if (!success)
            {
                return new ValidationResult(
                    $"Поле должно принимать одно из следующих допустимых значений: {string.Join(",", allowedValues.Select(v => v.EnumToInt()))}")
                ;
            }
            return ValidationResult.Success;
        }
    }
}