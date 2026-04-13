using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.Extensions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount
{
    [InjectAsSingleton(typeof(ITransferFromAccountUpdater))]
    internal sealed class TransferFromAccountUpdater : ITransferFromAccountUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderIncomingTransferFromAccount;
        private readonly ITransferFromAccountReader reader;
        private readonly TransferFromAccountLinksGetter linksGetter;
        private readonly TransferFromAccountApiClient apiClient;
        private readonly TransferFromAccountEventWriter writer;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly ITransferFromAccountCreator creator;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public TransferFromAccountUpdater(
            ITransferFromAccountReader reader,
            TransferFromAccountLinksGetter linksGetter,
            TransferFromAccountApiClient apiClient,
            TransferFromAccountEventWriter writer,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            ITransferFromAccountCreator creator,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.reader = reader;
            this.linksGetter = linksGetter;
            this.apiClient = apiClient;
            this.writer = writer;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(TransferFromAccountSaveRequest request)
        {
            var transferToAccountLink = await GetLinkedPaymentAsync(request.DocumentBaseId);
            if (transferToAccountLink != null)
            {
                await ShortUpdateAsync(request);
                return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
            }

            try
            {
                var operationType = await paymentOrderGetter.GetOperationTypeAsync(request.DocumentBaseId);                if (operationType != OperationType)                    throw new OperationMismatchTypeExcepton { ActualType = operationType };                await UpdateOperationAsync(request);
            }
            catch (OperationMismatchTypeExcepton omtex)
            {
                await RecreateOperationAsync(request, omtex.ActualType);
            }

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        public async Task UpdateSumAsync(long documentBaseId, decimal sum)
        {
            var transferFromAccount = await reader.GetByBaseIdAsync(documentBaseId);

            var sumSaveRequest = TransferFromAccountMapper.Map(transferFromAccount);
            sumSaveRequest.Sum = sum;

            await apiClient.UpdateAsync(sumSaveRequest);
            await writer.WriteUpdatedEventAsync(sumSaveRequest);
        }

        private async Task UpdateOperationAsync(TransferFromAccountSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);
        }

        private async Task RecreateOperationAsync(TransferFromAccountSaveRequest request, OperationType oldOperationType)
        {
            var oldDocumentBaseId = request.DocumentBaseId;
            var response = await creator.CreateAsync(request);
            await remover.DeleteAsync(oldDocumentBaseId, response.DocumentBaseId);
            await operationEventWriter.WriteOperationTypeChangedEventAsync(
                oldDocumentBaseId,
                oldOperationType,
                response.DocumentBaseId,
                OperationType);
        }

        private async Task ShortUpdateAsync(TransferFromAccountSaveRequest request)
        {
            var transferFromAccount = await reader.GetByBaseIdAsync(request.DocumentBaseId);

            var shortSaveRequest = TransferFromAccountMapper.Map(transferFromAccount);
            shortSaveRequest.Date = request.Date;
            shortSaveRequest.Number = request.Number;

            await apiClient.UpdateAsync(shortSaveRequest);
            await writer.WriteUpdatedEventAsync(shortSaveRequest);
        }

        private async Task<PaymentOrderLink> GetLinkedPaymentAsync(long documentBaseId)
        {
            var links = await linksGetter.GetAsync(documentBaseId);
            return links.TransferToAccount.GetOrThrow();
        }
    }
}