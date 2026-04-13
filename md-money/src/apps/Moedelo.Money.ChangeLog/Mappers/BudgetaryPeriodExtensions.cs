using System;
using System.Globalization;
using System.Linq;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;

namespace Moedelo.Money.ChangeLog.Mappers
{
    internal static class BudgetaryPeriodExtensions
    {
        private static readonly string[] MonthNames =
            CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.MonthNames
                .Select(monthName => CultureInfo.InvariantCulture.TextInfo.ToTitleCase(monthName))
                .ToArray();

        internal static string ToStringPeriodValue(this BudgetaryPeriod value)
        {
            return value.Type switch
            {
                BudgetaryPeriodType.None => "Не задан",
                BudgetaryPeriodType.Year => $"ГД - Год {value.Year}",
                BudgetaryPeriodType.HalfYear => $"ПЛ - Полугодие {value.Number:0} {value.Year}",
                BudgetaryPeriodType.Quarter => $"КВ - Квартал {value.Number:0} {value.Year}",
                BudgetaryPeriodType.Month => $"МС - Месяц {value.Number:00} - {GetMonthName(value.Number)} {value.Year}",
                BudgetaryPeriodType.NoPeriod => "0 - Без периода",
                BudgetaryPeriodType.Date => $"{value.Date:dd.MM.yyyy}",
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private static string GetMonthName(int number)
        {
            if (number < 1 || MonthNames.Length <= number)
            {
                return number.ToString("00");
            }

            return MonthNames[number - 1];
        }

        public static string FormatDate109(this string date)
        {
            return DateTime.TryParse(date, out var parsedDate)
                ? parsedDate.ToString("dd.MM.yyyy")
                : date;
        }
    }
}
