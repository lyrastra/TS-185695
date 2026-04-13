using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.ClosedPeriod;
using Moedelo.Money.Business.FirmRequisites;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.ClosedPeriods
{
    [InjectAsSingleton(typeof(IClosedPeriodReader))]
    internal class ClosedPeriodReader : IClosedPeriodReader
    {
        private readonly AsyncLocal<DateTime?> lastClosedPeriodDate = new AsyncLocal<DateTime?>();
        private readonly AsyncLocal<DateTime?> balancesDate = new AsyncLocal<DateTime?>();

        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IClosedPeriodApiClient closedPeriodApiClient;
        private readonly IBalancesApiClient balancesApiClient;
        private readonly IFirmRequisitesReader firmRequisitesReader;

        public ClosedPeriodReader(
            IExecutionInfoContextAccessor contextAccessor,
            IClosedPeriodApiClient closedPeriodApiClient,
            IBalancesApiClient balancesApiClient,
            IFirmRequisitesReader firmRequisitesReader)
        {
            this.contextAccessor = contextAccessor;
            this.closedPeriodApiClient = closedPeriodApiClient;
            this.balancesApiClient = balancesApiClient;
            this.firmRequisitesReader = firmRequisitesReader;
        }

        public async Task<DateTime?> GetLastClosedPeriodDateAsync()
        {
            if (lastClosedPeriodDate.Value == null)
            {
                var context = contextAccessor.ExecutionInfoContext;
                var lastPeriod = await closedPeriodApiClient.GetLastClosedPeriodAsync(context.FirmId, context.UserId).ConfigureAwait(false);
                lastClosedPeriodDate.Value = lastPeriod?.EndDate;
            }

            return lastClosedPeriodDate.Value;
        }

        public async Task<DateTime?> GetBalancesDateAsync()
        {
            if (balancesDate.Value == null)
            {
                var context = contextAccessor.ExecutionInfoContext;
                var commonBalanceDate = await balancesApiClient.GetDateAsync(context.FirmId, context.UserId).ConfigureAwait(false);
                if (commonBalanceDate.HasValue)
                {
                    //учитываем только годовые мастера ввода остатков
                    if (commonBalanceDate.Value.Day == 31 && commonBalanceDate.Value.Month == 12)
                    {
                        balancesDate.Value = commonBalanceDate;
                    }
                    else
                    {
                        balancesDate.Value = new DateTime(commonBalanceDate.Value.Year - 1, 12, 31);
                    }
                }
            }

            return balancesDate.Value;
        }

        public async Task<DateTime> GetLastClosedDateAsync()
        {
            return
                await GetLastClosedPeriodDateAsync() ??
                await GetBalancesDateAsync() ??
                await GetRegistrationDate() ??
                DateTime.MinValue;
        }

        public async Task<DateTime?> GetRegistrationDate()
        {
            var regDate = await firmRequisitesReader.GetRegistrationDateAsync();
            if (regDate == null || regDate == DateTime.MinValue)
            {
                return null;
            }

            return regDate.Value.AddDays(-1);
        }
    }
}
