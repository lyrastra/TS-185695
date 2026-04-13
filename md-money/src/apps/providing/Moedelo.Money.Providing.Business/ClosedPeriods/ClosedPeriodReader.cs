using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.ClosedPeriods
{
    [InjectAsSingleton(typeof(ClosedPeriodReader))]
    class ClosedPeriodReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IClosedPeriodApiClient closedPeriodApiClient;

        public ClosedPeriodReader(
            IExecutionInfoContextAccessor contextAccessor,
            IClosedPeriodApiClient closedPeriodApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.closedPeriodApiClient = closedPeriodApiClient;
        }

        public async Task<DateTime?> GetLastClosedPeriodDateAsync()
        {
            var context = contextAccessor.ExecutionInfoContext;
            var lastPeriod = await closedPeriodApiClient.GetLastClosedPeriodAsync(context.FirmId, context.UserId).ConfigureAwait(false);
            return lastPeriod?.EndDate.Date;
        }
    }
}
