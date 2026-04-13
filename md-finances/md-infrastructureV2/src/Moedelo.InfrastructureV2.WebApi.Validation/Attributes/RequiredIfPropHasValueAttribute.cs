using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    [Obsolete("Используй RequiredIfPropEqualsTo")]
    public class RequiredIfPropHasValue : RequiredAttribute
    {
        private string PropName { get; }
        private object DesiredValue { get; }

        public RequiredIfPropHasValue(string propName, object propValue, string errorMessage) : this(propName, propValue)
        {
            ErrorMessage = errorMessage;
        }

        public RequiredIfPropHasValue(string propName, object propValue)
        {
            PropName = propName;
            DesiredValue = propValue;
            ErrorMessage = $"Обязательно, если {propName} = {propValue}";
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            object instance = context.ObjectInstance;
            Type type = instance.GetType();

            object proprtyValue = type.GetProperty(PropName).GetValue(instance, null);
            if (DesiredValue != proprtyValue)
            {
                return ValidationResult.Success;
            }

            if (DesiredValue == null || proprtyValue.ToString() == DesiredValue.ToString())
            {
                return base.IsValid(value, context);
            }

            return ValidationResult.Success;
        }
    }
}