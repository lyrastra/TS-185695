using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionalEntityIdAttribute : ValidationAttribute
    {
        public OptionalEntityIdAttribute() : base("Значение должно быть ненулевым целочисленным идентификатором")
        {
        }

        public override bool IsValid(object value)
        {
            return value == null || Convert.ToDecimal(value) > 0;
        }
    }
}