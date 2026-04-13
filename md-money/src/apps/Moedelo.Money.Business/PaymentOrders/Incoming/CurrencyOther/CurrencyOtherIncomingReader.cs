using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyOther
{
    [InjectAsSingleton(typeof(ICurrencyOtherIncomingReader))]
    internal sealed class CurrencyOtherIncomingReader : ICurrencyOtherIncomingReader
    {
        private readonly CurrencyOtherIncomingApiClient apiClient;
        private readonly CurrencyOtherIncomingLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public CurrencyOtherIncomingReader(
            CurrencyOtherIncomingApiClient apiClient,
            CurrencyOtherIncomingLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<CurrencyOtherIncomingResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId);
            var documents = await linksGetter.GetAsync(documentBaseId);
            response.Contract = documents.Contract;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
