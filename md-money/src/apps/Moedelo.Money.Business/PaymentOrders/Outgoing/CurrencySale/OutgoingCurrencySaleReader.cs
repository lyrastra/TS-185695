using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale
{
    [InjectAsSingleton(typeof(IOutgoingCurrencySaleReader))]
    internal sealed class OutgoingCurrencySaleReader : IOutgoingCurrencySaleReader
    {
        private readonly OutgoingCurrencySaleApiClient apiClient;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public OutgoingCurrencySaleReader(
            OutgoingCurrencySaleApiClient apiClient,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<OutgoingCurrencySaleResponse> GetByBaseIdAsync(long baseId)
        {
            var response = await apiClient.GetAsync(baseId);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}