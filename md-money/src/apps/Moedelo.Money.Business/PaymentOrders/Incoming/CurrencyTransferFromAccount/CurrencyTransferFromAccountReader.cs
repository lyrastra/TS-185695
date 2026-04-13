using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    [InjectAsSingleton(typeof(ICurrencyTransferFromAccountReader))]
    internal sealed class CurrencyTransferFromAccountReader : ICurrencyTransferFromAccountReader
    {
        private readonly CurrencyTransferFromAccountApiClient apiClient;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public CurrencyTransferFromAccountReader(
            CurrencyTransferFromAccountApiClient apiClient,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<CurrencyTransferFromAccountResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            
            return response;
        }
    }
}