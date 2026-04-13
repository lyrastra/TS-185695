using System;
using System.Globalization;

namespace Moedelo.Money.Reports.Business.Extensions
{
    internal static class DateTimeExtensions
    {
        private static readonly CultureInfo CultureInfo = CultureInfo.GetCultureInfo("RU-ru");

        public static string GetMonthGenitiveName(this DateTime date)
        {
            return CultureInfo.DateTimeFormat.MonthGenitiveNames[date.Month - 1];
        }
    }
}
