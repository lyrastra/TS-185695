using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.ClosedPeriod;
using Moedelo.Money.Business.Abstractions.Exceptions;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Validation
{
    [InjectAsSingleton(typeof(IClosedPeriodValidator))]
    internal sealed class ClosedPeriodValidator : IClosedPeriodValidator
    {
        private static readonly DateTime minDocDate = new DateTime(2013, 01, 01);
        private readonly IClosedPeriodReader closedPeriodReader;

        public ClosedPeriodValidator(
            IClosedPeriodReader closedPeriodReader)
        {
            this.closedPeriodReader = closedPeriodReader;
        }

        public async Task ValidateAsync(DateTime date, string propName = null)
        {
            propName ??= "Date";

            if (date <= minDocDate.Date)
            {
                throw new BusinessValidationException(propName, $"Нельзя создавать п/п с датой ранее {minDocDate:yyyy-MM-dd}")
                {
                    Reason = ValidationFailedReason.ClosedPeriod
                };
            }

            var lastClosedDate = await closedPeriodReader.GetLastClosedPeriodDateAsync();
            if (lastClosedDate.HasValue && date.Date <= lastClosedDate.Value.Date)
            {
                throw new BusinessValidationException(propName, $"Дата попадает в закрытый период (до {lastClosedDate:yyyy-MM-dd})")
                {
                    Reason = ValidationFailedReason.ClosedPeriod
                };
            }

            var balancesDate = await closedPeriodReader.GetBalancesDateAsync();
            if (balancesDate.HasValue && date.Date <= balancesDate.Value.Date)
            {
                throw new BusinessValidationException(propName, $"Дата документа ранее даты ввода остатков ({balancesDate:yyyy-MM-dd})")
                {
                    Reason = ValidationFailedReason.ClosedPeriod
                };
            }
        }
    }
}
