using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DecimalMaxPrecisionAttribute : ValidationAttribute
    {
        private readonly string decimalSeparator = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;

        private readonly uint precision;

        public DecimalMaxPrecisionAttribute(uint precision):
            this(precision, $"Не может иметь больше {precision} знаков после запятой")
        {
        }

        public DecimalMaxPrecisionAttribute(uint precision, string errorMessage) : base(errorMessage)
        {
            this.precision = precision;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            var strValue = value.ToString().Replace(".", decimalSeparator).Replace(".", decimalSeparator);

            var separatorPosition = strValue.IndexOf(decimalSeparator, StringComparison.Ordinal);

            var valuePrecision = (separatorPosition == -1) ? 0 : strValue.Length - separatorPosition - 1;

            return valuePrecision <= precision;
        }
    }
}