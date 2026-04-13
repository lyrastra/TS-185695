using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromPurse
{
    [InjectAsSingleton(typeof(ITransferFromPurseCreator))]
    internal sealed class TransferFromPurseCreator : ITransferFromPurseCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly TransferFromPurseApiClient apiClient;
        private readonly TransferFromPurseEventWriter writer;

        public TransferFromPurseCreator(
            IBaseDocumentCreator baseDocumentCreator,
            TransferFromPurseApiClient apiClient,
            TransferFromPurseEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(TransferFromPurseSaveRequest request)
        {
            var baseDocumentCreateRequest = TransferFromPurseMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}