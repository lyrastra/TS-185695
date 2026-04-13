using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Registry;
using Moedelo.Money.Domain.Registry;
using Moedelo.Money.Domain.SelfCostPayments;

namespace Moedelo.Money.Business.Registry
{
    [InjectAsSingleton(typeof(IRegistryReader))]
    internal sealed class RegistryReader : IRegistryReader
    {
        private readonly IRegistryApiClient registryApiClient;

        public RegistryReader(IRegistryApiClient registryApiClient)
        {
            this.registryApiClient = registryApiClient;
        }

        public Task<RegistryResponse> GetAsync(RegistryQuery query)
        {
            return registryApiClient.GetAsync(query);
        }
        
        public Task<IReadOnlyCollection<RegistryOperation>> GetOutgoingPaymentsForTaxWidgetsAsync(DateTime startDate, DateTime endDate)
        {
            return registryApiClient.GetOutgoingPaymentsForTaxWidgetsAsync(startDate, endDate);
        }

        public Task<IReadOnlyCollection<SelfCostPayment>> GetSelfCostSettlementAccountPaymentsAsync(SelfCostPaymentRequest request)
        {
            return registryApiClient.GetSelfCostSettlementAccountPaymentsAsync(request);
        }

        public Task<IReadOnlyCollection<SelfCostPayment>> GetSelfCostCashPaymentsAsync(SelfCostPaymentRequest request)
        {
            return registryApiClient.GetSelfCostCashPaymentsAsync(request);
        }
    }
}
