using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.CashOrders;
using Moedelo.Money.Business.Abstractions.CashOrders.Incoming.RetailRevenue;
using Moedelo.Money.Domain.CashOrders.Incoming.RetailRevenue;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Incoming.RetailRevenue
{
    [InjectAsSingleton(typeof(IRetailRevenueReader))]
    internal class RetailRevenueReader : IRetailRevenueReader
    {
        private readonly RetailRevenueApiClient apiClient;
        private readonly ICashOrderAccessor cashOrderAccessor;

        public RetailRevenueReader(
            RetailRevenueApiClient apiClient,
            ICashOrderAccessor cashOrderAccessor)
        {
            this.apiClient = apiClient;
            this.cashOrderAccessor = cashOrderAccessor;
        }

        public async Task<RetailRevenueResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            response.IsReadOnly = cashOrderAccessor.IsReadOnly(response.ProvideInAccounting);
            return response;
        }
    }
}
