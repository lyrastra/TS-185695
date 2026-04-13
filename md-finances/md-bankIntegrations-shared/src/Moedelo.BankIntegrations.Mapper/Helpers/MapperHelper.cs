using System;
using System.Globalization;

namespace Moedelo.BankIntegrations.Mapper.Helpers
{
    public static class MapperHelper
    {
        public const string UtcZFormatDate = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'";

        public static string ToUtcZFormatDate(this DateTime dateTime)
        {
            return dateTime.ToUniversalTime().ToString(UtcZFormatDate);
        }

        public static DateTime ParseToDateTime(this string dateTime)
        {
            return DateTime.ParseExact(dateTime, UtcZFormatDate, CultureInfo.InvariantCulture).ToLocalTime();
        }

        public static DateTime? ParseToNullableDateTime(this string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime))
                return null;
            return dateTime.ParseToDateTime();
        }
    }
}
