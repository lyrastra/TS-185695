using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale
{
    [InjectAsSingleton(typeof(IIncomingCurrencySaleReader))]
    internal sealed class IncomingCurrencySaleReader : IIncomingCurrencySaleReader
    {
        private readonly IncomingCurrencySaleApiClient apiClient;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public IncomingCurrencySaleReader(
            IncomingCurrencySaleApiClient apiClient,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<IncomingCurrencySaleResponse> GetByBaseIdAsync(long baseId)
        {
            var response = await apiClient.GetAsync(baseId);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}