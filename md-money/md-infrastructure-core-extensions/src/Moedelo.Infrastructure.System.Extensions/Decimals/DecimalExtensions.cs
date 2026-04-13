using System;
using System.Globalization;

namespace Moedelo.Infrastructure.System.Extensions.Decimals
{
    public static class DecimalExtensions
    {
        private static readonly CultureInfo CultureInfo = CultureInfo.GetCultureInfo("RU-ru");

        public static decimal CurrencyRound(this decimal value, int digitCount = 2)
        {
            return Math.Round(value, digitCount, MidpointRounding.AwayFromZero);
        }

        public static string ToClientString(this decimal value)
        {
            return value.ToString("N", (IFormatProvider) DecimalExtensions.CultureInfo);
        }

        public static string ToClientStringWithoutDecimalPart(this decimal value)
        {
            return value.ToString("N", (IFormatProvider) DecimalExtensions.CultureInfo).Split(',')[0];
        }
    }
}