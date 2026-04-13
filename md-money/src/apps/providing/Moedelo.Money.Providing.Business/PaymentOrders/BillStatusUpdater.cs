using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(BillStatusUpdater))]
    class BillStatusUpdater
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly ISalesBillApiClient salesBillApiClient;

        public BillStatusUpdater(
            IExecutionInfoContextAccessor contextAccessor,
            ISalesBillApiClient salesBillApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.salesBillApiClient = salesBillApiClient;
        }

        public async Task UpdateStatusesAsync(IReadOnlyCollection<long> newBillBaseIds, IReadOnlyCollection<long> previousBillBaseIds)
        {
            var context = contextAccessor.ExecutionInfoContext;

            var billBaseIds = newBillBaseIds.Concat(previousBillBaseIds).Distinct().ToArray();
            await salesBillApiClient.UpdatePaymentStatusAsync(context.FirmId, context.UserId, billBaseIds);
        }
    }
}
