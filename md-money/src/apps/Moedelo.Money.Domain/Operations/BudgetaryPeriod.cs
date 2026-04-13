using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.Operations
{
    /// <summary>
    /// Общая модель бюджетного периода для БП и ЕНП
    /// </summary>
    public class BudgetaryPeriod
    {
        public BudgetaryPeriodType Type { get; set; }

        public DateTime? Date { get; set; }

        public int Number { get; set; }

        public int Year { get; set; }
    }

    public static class BudgetaryPeriodExtensions
    {
        public static DateTime GetEndDate(this BudgetaryPeriod period)
        {
            if (period.Type == BudgetaryPeriodType.Date && period.Date.HasValue)
            {
                return period.Date.Value;
            }
            var month = period.GetEndPeriodMonth();
            return new DateTime(period.Year, month, DateTime.DaysInMonth(period.Year, month));
        }

        public static string AsString(this BudgetaryPeriod period)
        {
            return period.Type switch
            {
                BudgetaryPeriodType.Quarter => $"{period.Number} квартал {period.Year} г",
                BudgetaryPeriodType.Month => new DateTime(period.Year, period.Number, 1).ToString("MMMM yyyy г"),
                BudgetaryPeriodType.HalfYear => $"{period.Number}-е полугодие {period.Year} г",
                BudgetaryPeriodType.Date => period.Date?.ToString("dd.MM.yyyy г") ?? string.Empty,
                _ => $"{period.Year} г",
            };
        }

        public static DateTime GetLastDay(this BudgetaryPeriod period, DateTime? paymentDate)
        {
            switch (period.Type)
            {
                case BudgetaryPeriodType.Year:
                case BudgetaryPeriodType.Quarter:
                case BudgetaryPeriodType.Month:
                case BudgetaryPeriodType.HalfYear:
                    var endPeriodMonth = period.GetEndPeriodMonth();
                    return new DateTime(period.Year, endPeriodMonth, DateTime.DaysInMonth(period.Year, endPeriodMonth));

                case BudgetaryPeriodType.Date:
                    return period.Date ?? DateTime.Today;

                default:
                    var year = paymentDate.HasValue
                        ? paymentDate.Value.Year
                        : DateTime.Today.Year;
                    return new DateTime(year, 12, 31);
            }
        }

        private static int GetEndPeriodMonth(this BudgetaryPeriod period)
        {
            return period.Type switch
            {
                BudgetaryPeriodType.Month => period.Number,
                BudgetaryPeriodType.Quarter => period.Number * 3,
                BudgetaryPeriodType.HalfYear => period.Number * 6,
                BudgetaryPeriodType.Date => (period.Date ?? DateTime.Today).Month,
                BudgetaryPeriodType.Year => 12,
                _ => 1,
            };
        }
    }
}
