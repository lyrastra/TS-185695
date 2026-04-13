using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IIncomingCurrencyPurchaseReader))]
    internal sealed class IncomingCurrencyPurchaseReader : IIncomingCurrencyPurchaseReader
    {
        private readonly IncomingCurrencyPurchaseApiClient apiClient;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public IncomingCurrencyPurchaseReader(
            IncomingCurrencyPurchaseApiClient apiClient,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<IncomingCurrencyPurchaseResponse> GetByBaseIdAsync(long baseId)
        {
            var response = await apiClient.GetAsync(baseId);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}