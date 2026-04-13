using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredPositiveNumberAttribute : RequiredAttribute
    {
        public RequiredPositiveNumberAttribute()
        {
            ErrorMessage = "Значение должно быть положительным числом";
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value) && Convert.ToDecimal(value) > 0;
        }
    }
}