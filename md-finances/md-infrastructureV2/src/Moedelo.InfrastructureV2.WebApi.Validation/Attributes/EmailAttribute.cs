using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class EmailAttribute : ValidationAttribute
    {
        private readonly EmailAddressAttribute raw = new EmailAddressAttribute();

        private const string EmailFormat = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validationResult = raw.GetValidationResult(value, validationContext);
            if (!string.IsNullOrEmpty(validationResult?.ErrorMessage))
            {
                return validationResult;
            }

            if (Regex.IsMatch(value.ToString(), @"[а-яА-Я]+"))
            {
                return new ValidationResult("Поле не может содержать буквы кириллицы");
            }
            
            if (!Regex.IsMatch(value.ToString(), EmailFormat,
                RegexOptions.ECMAScript | RegexOptions.IgnoreCase))
            {
                return new ValidationResult("Некорректный формат Email");
            }

            return ValidationResult.Success;
        }
    }
}