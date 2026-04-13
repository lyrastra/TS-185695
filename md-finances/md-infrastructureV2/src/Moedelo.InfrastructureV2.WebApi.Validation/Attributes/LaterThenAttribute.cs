using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    public class LaterThenAttribute : ValidationAttribute
    {
        private readonly string propName;

        public LaterThenAttribute(string propName)
        {
            this.propName =  propName;
            ErrorMessage = $"Дата должна быть позже, чем указанная в поле {propName}";
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            var current = value as DateTime?;
            var other = type.GetProperty(propName).GetValue(instance, null) as DateTime?;

            if (other > current)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}