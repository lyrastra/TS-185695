using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class NotEqualsWithAttribute : ValidationAttribute
    {
        private string PropName { get; }

        public NotEqualsWithAttribute(string propName, string errorMessage) : this(propName)
        {
            ErrorMessage = errorMessage;
        }

        public NotEqualsWithAttribute(string propName)
        {
            PropName = propName;
            ErrorMessage = $"Значение не должно совпадать со значением поля {propName}";
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            object proprtyValue = type.GetProperty(PropName).GetValue(instance, null);
            if (proprtyValue.ToString() == value?.ToString())
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}