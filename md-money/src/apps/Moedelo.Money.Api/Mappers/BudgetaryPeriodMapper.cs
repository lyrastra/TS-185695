using Moedelo.Money.Api.Models;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Api.Mappers
{
    internal static class BudgetaryPeriodMapper
    {
        public static BudgetaryPeriod MapToDomain(BudgetaryPeriodSaveDto period)
        {
            if (period.Type == BudgetaryPeriodType.Date)
            {
                return new BudgetaryPeriod
                {
                    Type = period.Type,
                    Date = period.Date.Value.Date,
                    Year = period.Date.Value.Year
                };
            }
            return new BudgetaryPeriod
            {
                Type = period.Type,
                Number = GetNumber(period),
                Year = period.Year
            };
        }

        public static BudgetaryPeriod MapToDomain(Kafka.Abstractions.Models.BudgetaryPeriod period)
        {
            if (period.Type == BudgetaryPeriodType.Date)
            {
                return new BudgetaryPeriod
                {
                    Type = period.Type,
                    Date = period.Date.Value.Date,
                    Year = period.Date.Value.Year
                };
            }
            return new BudgetaryPeriod
            {
                Type = period.Type,
                Number = period.Number,
                Year = period.Year
            };
        }

        public static BudgetaryPeriodResponseDto MapToDto(BudgetaryPeriod period)
        {
            return new BudgetaryPeriodResponseDto
            {
                Type = period.Type,
                Date = GetDate(period),
                Month = GetMonthFromNumber(period),
                Quarter = GetQuarterFromNumber(period),
                HalfYear = GetHalfYearFromNumber(period),
                Year = GetYear(period)
            };
        }

        private static int GetNumber(BudgetaryPeriodSaveDto period)
        {
            return period.Type switch
            {
                BudgetaryPeriodType.HalfYear => period.HalfYear,
                BudgetaryPeriodType.Quarter => period.Quarter,
                BudgetaryPeriodType.Month => period.Month,
                _ => 0,
            };
        }

        private static DateTime? GetDate(BudgetaryPeriod period)
        {
            return period.Type == BudgetaryPeriodType.Date
                ? period.Date
                : null;
        }

        private static int GetMonthFromNumber(BudgetaryPeriod period)
        {
            return period.Type == BudgetaryPeriodType.Month
                ? period.Number
                : 0;
        }

        private static int GetQuarterFromNumber(BudgetaryPeriod period)
        {
            return period.Type switch
            {
                BudgetaryPeriodType.Quarter => period.Number,
                BudgetaryPeriodType.Month => (period.Number - 1) / 3 + 1,
                _ => 0,
            };
        }

        private static int GetHalfYearFromNumber(BudgetaryPeriod period)
        {
            return period.Type switch
            {
                BudgetaryPeriodType.HalfYear => period.Number,
                BudgetaryPeriodType.Quarter => (period.Number - 1) / 2 + 1,
                BudgetaryPeriodType.Month => (period.Number - 1) / 6 + 1,
                _ => 0,
            };
        }

        private static int GetYear(BudgetaryPeriod period)
        {
            return period.Type switch
            {
                BudgetaryPeriodType.None or
                BudgetaryPeriodType.NoPeriod or
                BudgetaryPeriodType.Date => 0,

                _ => period.Year,
            };
        }
    }
}
