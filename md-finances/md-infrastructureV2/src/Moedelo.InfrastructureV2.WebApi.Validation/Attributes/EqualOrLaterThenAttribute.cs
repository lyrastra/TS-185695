using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class EqualOrLaterThenAttribute : ValidationAttribute
    {
        private readonly string propName;

        public EqualOrLaterThenAttribute(string propName)
        {
            this.propName =  propName;
            ErrorMessage = $"Дата должна быть больше или равна той, что указана в поле {propName}";
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            var current = value as DateTime?;
            var other = type.GetProperty(propName).GetValue(instance, null) as DateTime?;

            return (!other.HasValue || current >= other)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage);
        }
    }
}