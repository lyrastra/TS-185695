using System;
using System.Data.SqlTypes;
using System.Globalization;

namespace Moedelo.CommonV2.Extensions.System
{
    public static class DateTimeEx
    {
        private static readonly CultureInfo RuCultureInfo = CultureInfo.CreateSpecificCulture("ru");

        /// <summary>
        /// Парсит дату, при неудаче возвращает DateTimeMin
        /// </summary>
        /// <param name="dateString">Строка для разбора</param>
        public static DateTime TryParseDate(this string dateString)
        {
            DateTime.TryParse(dateString, RuCultureInfo, DateTimeStyles.None, out DateTime date);
            return date;
        }
        
        /// <summary>
        /// Парсит дату (версия из v1)
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime TryParseDateV1(this string dateString)
        {
            DateTime.TryParse(dateString, out var result);
            return result;
        }
        
        public static DateTime? TryParseToNullableDateTime(this string inputDate)
        {
            DateTime? nullableDateTime = new DateTime?();
            DateTime result;
            if (DateTime.TryParse(inputDate, out result))
                nullableDateTime = new DateTime?(result);
            return nullableDateTime;
        }

        /// <summary>
        /// Безопасное значение для SqlDateTime c учетом разброса
        /// </summary>
        public static DateTime ToSafeSqlDate(this DateTime date, int dayOffset = 0)
        {
            if (date <= SqlDateTime.MinValue.Value)
            {
                date = (SqlDateTime.MinValue.Value).AddDays(dayOffset);
            }

            if ((SqlDateTime.MaxValue.Value - date).Days < dayOffset)
            {
                date = SqlDateTime.MaxValue.Value.AddDays(-dayOffset);
            }

            return date;
        }

        /// <summary>
        /// Парсит дату, при неудаче возвращает дефолтную дату
        /// </summary>
        public static DateTime ParseOrDefaultDate(this string date, DateTime defaultDate)
        {
            DateTime parsedDate;
            return DateTime.TryParse(date, out parsedDate) ? parsedDate : defaultDate;
        }

        /// <summary>
        /// Парсит дату, при неудаче возвращает SqlDateTimeMin
        /// </summary>
        /// <param name="dateString">Строка для разбора</param>
        public static DateTime TryParseSqlDate(this string dateString)
        {
            return ParseOrDefaultDate(dateString, SqlDateTime.MinValue.Value);
        }

        /// <summary>
        /// Переводит дату в формат "dd.MM.yyyy"
        /// </summary>
        /// <param name="date">Дата для конвертации</param>
        public static string ToClientString(this DateTime date)
        {
            return date.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Пример: 19 янв 2013
        /// </summary>
        public static string ToShortMixedString(this DateTime date)
        {
            return date.ToString("dd MMM yyyy").ToLower();
        }

        /// <summary>
        /// Получаем первый день для года
        /// </summary>
        public static DateTime GetStartDateForYear(this int year)
        {
            return new DateTime(year, 1, 1);
        }

        /// <summary>
        /// Получаем первый день для года
        /// </summary>
        public static DateTime GetStartDateForYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        /// <summary>
        /// Получаем последний день для года
        /// </summary>
        public static DateTime GetLastDateForYear(this int year)
        {
            return new DateTime(year, 12, 31);
        }

        /// <summary>
        /// Получаем последний день для года
        /// </summary>
        public static DateTime GetLastDateForYear(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }

        /// <summary>
        /// Получаем последний день для месяца
        /// </summary>
        public static DateTime GetMonthLastDate(this DateTime date)
        {
            var day = DateTime.DaysInMonth(date.Year, date.Month);
            return new DateTime(date.Year, date.Month, day);
        }

        /// <summary>
        /// Получаем первый день для месяца
        /// </summary>
        public static DateTime GetMonthFirstDate(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static bool IsMonthFirstDate(this DateTime date)
        {
            return date.Day == 1;
        }

        public static bool IsMonthLastDate(this DateTime date)
        {
            var lastDate = GetMonthLastDate(date);

            return date.Day == lastDate.Day;
        }

        /// <summary> Привести к дате </summary>
        /// <returns>Дата, String.Empty если null</returns>
        public static string ToShortDateString(this DateTime? value)
        {
            return value?.ToShortDateString() ?? string.Empty;
        }

        /// <summary>
        /// Конвертирует в точную строку
        /// </summary>
        public static string ToStrictString(this DateTime dateTime) => dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
    }
}
