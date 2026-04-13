using System.ComponentModel.DataAnnotations;
using System.Linq;
using Moedelo.InfrastructureV2.WebApi.Validation.Utils;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class RequiredOneOfValueAttribute : RequiredAttribute
    {
        private readonly object[] allowedValues;

        public RequiredOneOfValueAttribute(params object[] allowedValues)
        {
            this.allowedValues = allowedValues;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var success = value != null && allowedValues.Any(x => x.MatchValue(value));
            if (!success)
            {
                var allowedText = string.Join(",", allowedValues.Select(v => v.EnumToInt()));
                return new ValidationResult($"Поле должно принимать одно из следующих допустимых значений: {allowedText}");
            }
            return ValidationResult.Success;
        }
    }
}