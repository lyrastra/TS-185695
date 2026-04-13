using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(ITransferFromCashUpdater))]
    internal sealed class TransferFromCashUpdater : ITransferFromCashUpdater
    {
        private readonly OperationType OperationType = OperationType.MemorialWarrantTransferFromCash;
        private readonly TransferFromCashApiClient apiClient;
        private readonly TransferFromCashEventWriter writer;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly ITransferFromCashCreator creator;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public TransferFromCashUpdater(
            TransferFromCashApiClient apiClient,
            TransferFromCashEventWriter writer,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            ITransferFromCashCreator creator,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(TransferFromCashSaveRequest request)
        {
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

        private async Task RecreateOperationAsync(TransferFromCashSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(TransferFromCashSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);
        }
    }
}