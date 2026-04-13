using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.Infrastructure.AspNetCore.Xss.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ValidateXssAttribute : ValidationAttribute
    {
        public ValidateXssAttribute()
            : base("Обнаружено потенциально опасное содержимое.")
        {
        }

        public override bool IsValid(object value)
        {
            try
            {
                XssValidator.Validate(value);
                return true;
            }
            catch (XssValidationException)
            {
                return false;
            }
        }
    }
}
