using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(ITransferFromCashCreator))]
    internal sealed class TransferFromCashCreator : ITransferFromCashCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly TransferFromCashApiClient apiClient;
        private readonly TransferFromCashEventWriter writer;

        public TransferFromCashCreator(
            IBaseDocumentCreator baseDocumentCreator,
            TransferFromCashApiClient apiClient,
            TransferFromCashEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(TransferFromCashSaveRequest request)
        {
            var baseDocumentCreateRequest = TransferFromCashMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest).ConfigureAwait(false);

            await apiClient.CreateAsync(request).ConfigureAwait(false);
            await writer.WriteCreatedEventAsync(request).ConfigureAwait(false);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}