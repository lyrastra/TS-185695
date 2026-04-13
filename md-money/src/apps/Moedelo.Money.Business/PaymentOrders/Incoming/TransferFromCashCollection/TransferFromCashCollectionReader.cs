using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCashCollection;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCashCollection
{
    [InjectAsSingleton(typeof(ITransferFromCashCollectionReader))]
    internal sealed class TransferFromCashCollectionReader : ITransferFromCashCollectionReader
    {
        private readonly TransferFromCashCollectionApiClient apiClient;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public TransferFromCashCollectionReader(
            TransferFromCashCollectionApiClient apiClient,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<TransferFromCashCollectionResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}