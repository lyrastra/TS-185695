using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    [InjectAsSingleton(typeof(ICurrencyTransferToAccountReader))]
    internal sealed class CurrencyTransferToAccountReader : ICurrencyTransferToAccountReader
    {
        private readonly CurrencyTransferToAccountApiClient apiClient;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public CurrencyTransferToAccountReader(
            CurrencyTransferToAccountApiClient apiClient,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<CurrencyTransferToAccountResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
