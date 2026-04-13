using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class OgrnAttribute : ValidationAttribute
    {
        private static readonly Regex Ogrn = new Regex(@"^\d{13}$");
        private static readonly Regex Ogrnip = new Regex(@"^\d{15}$");

        protected override ValidationResult IsValid(object obj, ValidationContext validationContext)
        {
            var value = obj?.ToString();

            if (string.IsNullOrEmpty(value))
            {
                return ValidationResult.Success;
            }

            if (IsValidOgrn(value) || IsValidOgrnip(value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Неверный формат ОГРН");
        }

        private static bool IsValidOgrn(string textValue)
        {
            if (!Ogrn.IsMatch(textValue))
            {
                return false;
            }

            var value = long.Parse(textValue);

            return value%10 == (value/10)%11%10;
        }

        private static bool IsValidOgrnip(string textValue)
        {
            if (!Ogrnip.IsMatch(textValue))
            {
                return false;
            }

            var value = long.Parse(textValue);

            return value%10 == (value/10)%13%10;
        }
    }
}