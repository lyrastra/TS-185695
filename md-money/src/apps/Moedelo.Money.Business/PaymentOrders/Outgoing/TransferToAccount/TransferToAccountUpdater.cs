using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton(typeof(ITransferToAccountUpdater))]
    [InjectAsSingleton(typeof(ITransferToAccountUpdaterExt))]
    internal sealed class TransferToAccountUpdater : ITransferToAccountUpdater, ITransferToAccountUpdaterExt
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingTransferToAccount;
        private readonly TransferToAccountApiClient apiClient;
        private readonly TransferToAccountEventWriter writer;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly ITransferToAccountCreator creator;
        private readonly TransferToAccountLinksGetter linksGetter;
        private readonly ITransferFromAccountUpdater transferFromAccountUpdater;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public TransferToAccountUpdater(
            TransferToAccountApiClient apiClient,
            TransferToAccountEventWriter writer,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            ITransferToAccountCreator creator,
            TransferToAccountLinksGetter linksGetter,
            ITransferFromAccountUpdater transferFromAccountUpdater,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.linksGetter = linksGetter;
            this.transferFromAccountUpdater = transferFromAccountUpdater;
            this.operationEventWriter = operationEventWriter;
        }

        async Task<TransferToAccountSaveResponse>
            IPaymentOrderUpdater<TransferToAccountSaveRequest, TransferToAccountSaveResponse>
                .UpdateAsync(TransferToAccountSaveRequest request)
        {
            try
            {
                var operationType = await paymentOrderGetter.GetOperationTypeAsync(request.DocumentBaseId);                if (operationType != OperationType)                    throw new OperationMismatchTypeExcepton { ActualType = operationType };                return await UpdateOperationAsync(request);
            }
            catch (OperationMismatchTypeExcepton omtex)
            {
                return await RecreateOperationAsync(request, omtex.ActualType);
            }
        }

        async Task<PaymentOrderSaveResponse>
            IPaymentOrderUpdater<TransferToAccountSaveRequest, PaymentOrderSaveResponse>.
                UpdateAsync(TransferToAccountSaveRequest request)
        {
            var updater = this as IPaymentOrderUpdater<TransferToAccountSaveRequest, TransferToAccountSaveResponse>;
            var response = await updater.UpdateAsync(request);

            return response;
        }

        private async Task<TransferToAccountSaveResponse> RecreateOperationAsync(TransferToAccountSaveRequest request, OperationType oldOperationType)
        {
            var oldDocumentBaseId = request.DocumentBaseId;
            var response = await creator.CreateWithIncomingAsync(request);
            await remover.DeleteAsync(oldDocumentBaseId, response.DocumentBaseId);
            await operationEventWriter.WriteOperationTypeChangedEventAsync(
                oldDocumentBaseId,
                oldOperationType,
                response.DocumentBaseId,
                OperationType);

            return response;
        }

        private async Task<TransferToAccountSaveResponse> UpdateOperationAsync(TransferToAccountSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            var transferFromAccountBaseId = await UpdateLinkedPaymentAsync(request);
            await writer.WriteUpdatedEventAsync(request);
            return new TransferToAccountSaveResponse
            {
                DocumentBaseId = request.DocumentBaseId,
                TransferFromAccountBaseId = transferFromAccountBaseId
            };
        }

        private async Task<long> UpdateLinkedPaymentAsync(TransferToAccountSaveRequest request)
        {
            var transferFromAccountLink = await GetLinkedPaymentAsync(request.DocumentBaseId);
            if (transferFromAccountLink == null)
            {
                return 0;
            }

            var transferFromAccountBaseId = transferFromAccountLink.DocumentBaseId;
            await transferFromAccountUpdater.UpdateSumAsync(transferFromAccountBaseId, request.Sum);
            request.TransferFromAccountBaseId = transferFromAccountBaseId;

            return transferFromAccountBaseId;
        }

        private async Task<PaymentOrderLink> GetLinkedPaymentAsync(long documentBaseId)
        {
            var links = await linksGetter.GetAsync(documentBaseId);
            return links.TransferFromAccount.GetOrThrow();
        }
    }
}