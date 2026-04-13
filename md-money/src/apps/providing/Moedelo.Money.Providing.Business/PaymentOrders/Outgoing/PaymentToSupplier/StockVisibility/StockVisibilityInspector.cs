using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.ApiClient.Abstractions.Clients;
using System.Threading.Tasks;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Outgoing.PaymentToSupplier.StockVisibility
{
    [InjectAsSingleton(typeof(StockVisibilityInspector))]
    internal class StockVisibilityInspector
    {
        private readonly IStockVisibilityApiClient client;

        public StockVisibilityInspector(
            IStockVisibilityApiClient client)
        {
            this.client = client;
        }

        public async Task<bool> IsStockInVisible(int year)
        {
            var IsStockVisible = await client.IsVisibleAsync(year);
            return !IsStockVisible;
        }
    }
}
