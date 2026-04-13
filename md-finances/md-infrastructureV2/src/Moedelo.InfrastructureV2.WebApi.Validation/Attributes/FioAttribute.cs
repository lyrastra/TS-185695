using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class FioAttribute : ValidationAttribute
    {
        private const string Format = @"^[а-яА-ЯёЁ-]{1,50}$";
        
        protected override ValidationResult IsValid(object obj, ValidationContext validationContext)
        {
            var value = obj?.ToString();

            if (string.IsNullOrEmpty(value))
            {
                return ValidationResult.Success;
            }

            if (!Regex.IsMatch(value, Format))
            {
                return new ValidationResult("Поле может содержать только буквы или дефис, и должно быть содержать не более 50 символов");
            }

            return ValidationResult.Success;
        }
    }
}