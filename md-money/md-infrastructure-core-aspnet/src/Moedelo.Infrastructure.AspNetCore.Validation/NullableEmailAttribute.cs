using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Moedelo.Infrastructure.AspNetCore.Validation
{
    public class NullableEmailAttribute : ValidationAttribute
    {
        private readonly EmailAddressAttribute raw = new EmailAddressAttribute();

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var validationResult = raw.GetValidationResult(value, validationContext);
            if (!string.IsNullOrEmpty(validationResult?.ErrorMessage))
            {
                return validationResult;
            }

            if (Regex.IsMatch(value.ToString(), @"[а-яА-Я]+"))
            {
                return new ValidationResult("Поле не может содержать буквы кириллицы.");
            }

            return ValidationResult.Success;
        }
    }
}