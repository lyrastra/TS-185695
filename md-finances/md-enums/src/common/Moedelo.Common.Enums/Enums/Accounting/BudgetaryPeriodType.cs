using System;

namespace Moedelo.Common.Enums.Enums.Accounting
{
    public enum BudgetaryPeriodType
    {
        /// <summary> Не указан период </summary>
        None = 0,

        /// <summary> Год (ГД) </summary>
        Year,

        /// <summary> Полугодие (ПЛ) </summary>
        HalfYear,

        /// <summary> Квартал (КВ) </summary>
        Quarter,

        /// <summary> Месяц (МС) </summary>
        Month,

        /// <summary> Первая декада месяца </summary>
        Decade1,

        /// <summary> Вторая декада месяца </summary>
        Decade2,

        /// <summary> Третья декада месяца </summary>
        Decade3,

        /// <summary> 0 - Без периода </summary>
        NoPeriod,

        /// <summary> Указана конкретная дата </summary>
        Date
    }

    public static class PaymentPeriodTypeExtensions
    {
        /// <summary> Получить сокращение периода платежа: ГД, ПЛ, КВ или МС </summary>
        public static string GetAbbreviation(this BudgetaryPeriodType type)
        {
            switch (type)
            {
                case BudgetaryPeriodType.Year:
                    return "ГД";
                case BudgetaryPeriodType.HalfYear:
                    return "ПЛ";
                case BudgetaryPeriodType.Quarter:
                    return "КВ";
                case BudgetaryPeriodType.Month:
                    return "МС";
                case BudgetaryPeriodType.Decade1:
                    return "Д1";
                case BudgetaryPeriodType.Decade2:
                    return "Д2";
                case BudgetaryPeriodType.Decade3:
                    return "Д3";
                case BudgetaryPeriodType.NoPeriod:
                    return "0";
                case BudgetaryPeriodType.Date:
                    return "ДТ";
                default:
                    return string.Empty;
            }
        }

        /// <summary> Получить тип периода по сокращению (ГД, ПЛ, КВ или МС) </summary>
        public static BudgetaryPeriodType GetPaymentPeriodType(string abbreviation)
        {
            if (string.IsNullOrEmpty(abbreviation))
            {
                return BudgetaryPeriodType.None;
            }

            if (abbreviation.StartsWith("ГД", StringComparison.OrdinalIgnoreCase))
            {
                return BudgetaryPeriodType.Year;
            }

            if (abbreviation.StartsWith("ПЛ", StringComparison.OrdinalIgnoreCase))
            {
                return BudgetaryPeriodType.HalfYear;
            }

            if (abbreviation.StartsWith("КВ", StringComparison.OrdinalIgnoreCase))
            {
                return BudgetaryPeriodType.Quarter;
            }

            if (abbreviation.StartsWith("МС", StringComparison.OrdinalIgnoreCase))
            {
                return BudgetaryPeriodType.Month;
            }

            if (abbreviation.StartsWith("Д1", StringComparison.OrdinalIgnoreCase))
            {
                return BudgetaryPeriodType.Decade1;
            }

            if (abbreviation.StartsWith("Д2", StringComparison.OrdinalIgnoreCase))
            {
                return BudgetaryPeriodType.Decade2;
            }

            if (abbreviation.StartsWith("Д3", StringComparison.OrdinalIgnoreCase))
            {
                return BudgetaryPeriodType.Decade3;
            }

            if (abbreviation.StartsWith("0", StringComparison.OrdinalIgnoreCase))
            {
                return BudgetaryPeriodType.NoPeriod;
            }

            if (abbreviation.StartsWith("ДТ", StringComparison.OrdinalIgnoreCase))
            {
                return BudgetaryPeriodType.Date;
            }

            return BudgetaryPeriodType.None;
        }
    }

}