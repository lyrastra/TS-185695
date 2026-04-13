using System;
using System.Globalization;

namespace Moedelo.Money.Business.Validation.SimpleValidators
{
    public static class MaxPrecisionSumValidator
    {
        public static string Validate(string memberName, decimal value)
        {
            string decimalSeparator = NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;

            uint precision = 2;

            var strValue = value.ToString().Replace(".", decimalSeparator).Replace(".", decimalSeparator);

            var separatorPosition = strValue.IndexOf(decimalSeparator, StringComparison.Ordinal);

            var valuePrecision = (separatorPosition == -1) ? 0 : strValue.Length - separatorPosition - 1;

            if (valuePrecision > precision)
            {
                return $"Не может иметь больше {precision} знаков после запятой";
            }

            return null;
        }
    }
}
