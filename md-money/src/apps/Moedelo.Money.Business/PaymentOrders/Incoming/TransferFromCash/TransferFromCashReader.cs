using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(ITransferFromCashReader))]
    internal sealed class TransferFromCashReader : ITransferFromCashReader
    {
        private readonly TransferFromCashApiClient apiClient;
        private readonly TransferFromCashLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public TransferFromCashReader(
            TransferFromCashApiClient apiClient,
            TransferFromCashLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<TransferFromCashResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            var links = await linksGetter.GetByBaseIdAsync(documentBaseId).ConfigureAwait(false);
            response.CashOrder = links.CashOrder;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}