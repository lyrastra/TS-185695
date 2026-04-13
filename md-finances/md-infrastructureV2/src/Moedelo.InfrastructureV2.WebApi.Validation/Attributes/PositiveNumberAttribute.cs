using System;
using System.ComponentModel.DataAnnotations;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PositiveNumberAttribute : ValidationAttribute
    {
        public bool AllowNull { get; set; }

        public PositiveNumberAttribute() : this("Значение должно быть положительным числом")
        {
        }

        public PositiveNumberAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return AllowNull;
            }

            return Convert.ToDecimal(value) > 0;
        }
    }
}