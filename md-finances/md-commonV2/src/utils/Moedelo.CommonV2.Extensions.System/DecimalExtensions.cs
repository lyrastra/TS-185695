using System;

namespace Moedelo.CommonV2.Extensions.System
{
    public static class DecimalExtensions
    {
        /// <summary>
        /// Математическое округление суммы (по умолчанию до 2 знаков после запятой)
        /// </summary>
        public static decimal CurrencyRound(this decimal sum, int precision = 2)
        {
            return Math.Round(sum, precision, MidpointRounding.AwayFromZero);
        }
    }
}