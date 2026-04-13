using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.CommonV2.Xss.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class XssValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        try
        {
            XssValidator.Validate(value);
            return ValidationResult.Success;
        }
        catch (XssValidationException ex)
        {
            return new ValidationResult(ex.Message, [ex.Suspicious.Path]);
        }
    }
}