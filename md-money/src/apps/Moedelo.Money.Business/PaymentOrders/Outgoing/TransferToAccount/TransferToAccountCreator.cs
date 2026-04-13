using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using System.Threading.Tasks;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton(typeof(ITransferToAccountCreator))]
    class TransferToAccountCreator : ITransferToAccountCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly ITransferFromAccountCreator transferFromAccountCreator;
        private readonly TransferToAccountApiClient apiClient;
        private readonly TransferToAccountEventWriter writer;

        public TransferToAccountCreator(
            IBaseDocumentCreator baseDocumentCreator,
            ITransferFromAccountCreator transferFromAccountCreator,
            TransferToAccountApiClient apiClient,
            TransferToAccountEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.transferFromAccountCreator = transferFromAccountCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(TransferToAccountSaveRequest request)
        {
            var baseDocumentCreateRequest = TransferToAccountMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        public async Task<TransferToAccountSaveResponse> CreateWithIncomingAsync(TransferToAccountSaveRequest request)
        {
            var baseDocumentCreateRequest = TransferToAccountMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);

            if (request.OperationState != OperationState.OutsourceProcessingValidationFailed)
            {
                var transferFromAccountSaveRequest = TransferToAccountMapper.MapToTransferFromAccountSaveRequest(request);
                var transferFromAccountSaveResponse = await transferFromAccountCreator.CreateAsync(transferFromAccountSaveRequest);
                request.TransferFromAccountBaseId = transferFromAccountSaveResponse.DocumentBaseId;
            }

            await writer.WriteCreatedEventAsync(request);

            return new TransferToAccountSaveResponse
            {
                DocumentBaseId = request.DocumentBaseId,
                TransferFromAccountBaseId = request.TransferFromAccountBaseId
            };
        }
    }
}