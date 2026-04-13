using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Domain.Operations;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(IBudgetaryPeriodValidator))]
    internal class BudgetaryPeriodValidator : IBudgetaryPeriodValidator
    {
        private const int BudgetaryPeriodMinYear = 2010;

        public Task ValidateAsync(BudgetaryPeriod period)
        {
            switch (period.Type)
            {
                case BudgetaryPeriodType.NoPeriod:
                    return Task.CompletedTask;

                case BudgetaryPeriodType.Year:
                    ValidateMinYear(period);
                    break;

                case BudgetaryPeriodType.HalfYear:
                    ValidateHalfYear(period);
                    break;

                case BudgetaryPeriodType.Quarter:
                    ValidateQuarter(period);
                    break;

                case BudgetaryPeriodType.Month:
                    ValidateMonth(period);
                    break;

                case BudgetaryPeriodType.Date:
                    ValidateDate(period);
                    break;

                default:
                    throw new BusinessValidationException("Period.Type", $"Значение {(int)period.Type} не поддерживается");
            }
            return Task.CompletedTask;
        }

        private static void ValidateMonth(BudgetaryPeriod period)
        {
            ValidateMinYear(period);
            if (period.Number < 1 || period.Number > 12)
            {
                throw new BusinessValidationException("Period.Month", $"Номер месяца должен быть в промежутке между 1 и 12");
            }
        }

        private static void ValidateQuarter(BudgetaryPeriod period)
        {
            ValidateMinYear(period);
            if (new[] { 1, 2, 3, 4 }.Contains(period.Number) == false)
            {
                throw new BusinessValidationException("Period.Quarter", $"Номер квартала должен быть в промежутке между 1 и 4");
            }
        }

        private static void ValidateHalfYear(BudgetaryPeriod period)
        {
            ValidateMinYear(period);
            if (new[] { 1, 2 }.Contains(period.Number) == false)
            {
                throw new BusinessValidationException("Period.HalfYear", "Номер полугодия должен быть равен 1 или 2");
            }
        }

        private static void ValidateDate(BudgetaryPeriod period)
        {
            if (period.Date == null)
            {
                throw new BusinessValidationException("Period.Date", $"Отсутствует дата");
            }
            if (period.Date.Value.Year < BudgetaryPeriodMinYear)
            {
                throw new BusinessValidationException("Period.Date", $"Не поддерживается дата ранее {BudgetaryPeriodMinYear} года");
            }
        }

        private static void ValidateMinYear(BudgetaryPeriod period)
        {
            if (period.Year < BudgetaryPeriodMinYear)
            {
                throw new BusinessValidationException("Period.Year", $"Не поддерживается период ранее {BudgetaryPeriodMinYear} года");
            }
        }
    }
}