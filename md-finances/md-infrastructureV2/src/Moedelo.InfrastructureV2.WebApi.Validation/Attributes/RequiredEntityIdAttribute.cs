using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredEntityIdAttribute : RequiredAttribute
    {
        public const string Message = "Значение должно быть ненулевым целочисленным идентификатором";

        public RequiredEntityIdAttribute()
        {
            ErrorMessage = Message;
        }

        public override bool IsValid(object value)
        {
            return base.IsValid(value) && Convert.ToDecimal(value) > 0;
        }
    }
}