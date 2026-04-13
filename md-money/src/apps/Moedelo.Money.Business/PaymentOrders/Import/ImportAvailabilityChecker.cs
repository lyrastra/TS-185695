using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.ClosedPeriod;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Import
{
    [InjectAsSingleton(typeof(ImportAvailabilityChecker))]
    class ImportAvailabilityChecker
    {
        private readonly IClosedPeriodReader closedPeriodReader;

        public ImportAvailabilityChecker(IClosedPeriodReader closedPeriodReader)
        {
            this.closedPeriodReader = closedPeriodReader;
        }

        public async Task<bool> IsAvailableAsync(DateTime date)
        {
            var lastClosedDate = await closedPeriodReader.GetLastClosedDateAsync();
            return date.Date > lastClosedDate.Date;
        }
    }
}
