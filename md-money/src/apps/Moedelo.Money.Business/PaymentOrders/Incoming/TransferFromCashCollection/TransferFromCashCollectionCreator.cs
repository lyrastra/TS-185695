using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCashCollection;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCashCollection
{
    [InjectAsSingleton(typeof(ITransferFromCashCollectionCreator))]
    class TransferFromCashCollectionCreator : ITransferFromCashCollectionCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly TransferFromCashCollectionApiClient apiClient;
        private readonly TransferFromCashCollectionEventWriter writer;

        public TransferFromCashCollectionCreator(
            IBaseDocumentCreator baseDocumentCreator,
            TransferFromCashCollectionApiClient apiClient,
            TransferFromCashCollectionEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(TransferFromCashCollectionSaveRequest request)
        {
            var baseDocumentCreateRequest = TransferFromCashCollectionMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest).ConfigureAwait(false);

            await apiClient.CreateAsync(request).ConfigureAwait(false);
            await writer.WriteCreatedEventAsync(request).ConfigureAwait(false);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}