using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Money.PaymentOrders.Business.Abstractions.KontragentSettlementAccounts;

namespace Moedelo.Money.PaymentOrders.Business.KontragentSettlementAccounts
{
    [InjectAsSingleton(typeof(IKontragentSettlementAccountsReader))]
    internal class KontragentSettlementAccountsReader : IKontragentSettlementAccountsReader
    {
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IKontragentSettlementAccountsApiClient kontragentSettlementAccountsApiClient;

        public KontragentSettlementAccountsReader(
            IExecutionInfoContextAccessor contextAccessor,
            IKontragentSettlementAccountsApiClient kontragentSettlementAccountsApiClient)
        {
            this.contextAccessor = contextAccessor;
            this.kontragentSettlementAccountsApiClient = kontragentSettlementAccountsApiClient;
        }

        public async Task<List<KontragentSettlementAccountDto>> GetByKontragentAsync(int kontragentId)
        {
            var context = contextAccessor.ExecutionInfoContext;
            return await kontragentSettlementAccountsApiClient.GetByKontragentAsync(context.FirmId, context.UserId, kontragentId);
        }
    }
}
