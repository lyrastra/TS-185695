using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IOutgoingCurrencyPurchaseReader))]
    internal sealed class OutgoingCurrencyPurchaseReader : IOutgoingCurrencyPurchaseReader
    {
        private readonly OutgoingCurrencyPurchaseApiClient apiClient;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public OutgoingCurrencyPurchaseReader(
            OutgoingCurrencyPurchaseApiClient apiClient,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<OutgoingCurrencyPurchaseResponse> GetByBaseIdAsync(long baseId)
        {
            var response = await apiClient.GetAsync(baseId);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}