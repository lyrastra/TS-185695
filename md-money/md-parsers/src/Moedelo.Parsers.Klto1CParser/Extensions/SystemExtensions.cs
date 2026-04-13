using System;
using System.Globalization;

namespace Moedelo.Parsers.Klto1CParser.Extensions
{
    internal static class SystemExtensions
    {
        public static DateTime? AsDateTime(this string value, DateTime? @default = null)
        {
            DateTime result;
            return DateTime.TryParse(value, out result)
                ? result
                : @default;
        }

        public static decimal? AsDecimal(this string value, decimal? @default = null)
        {
            decimal result;
            return decimal.TryParse(value?.Replace(",", "."), NumberStyles.Number, CultureInfo.InvariantCulture, out result)
                ? result
                : @default;
        }
    }
}
