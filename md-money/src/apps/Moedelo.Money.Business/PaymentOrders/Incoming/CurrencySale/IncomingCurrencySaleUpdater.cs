using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale
{
    [InjectAsSingleton(typeof(IIncomingCurrencySaleUpdater))]
    internal sealed class IncomingCurrencySaleUpdater : IIncomingCurrencySaleUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderIncomingCurrencySale;
        private readonly IncomingCurrencySaleApiClient apiClient;
        private readonly IIncomingCurrencySaleCreator creator;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IncomingCurrencySaleEventWriter writer;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public IncomingCurrencySaleUpdater(
            IncomingCurrencySaleApiClient apiClient,
            IIncomingCurrencySaleCreator creator,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IncomingCurrencySaleEventWriter writer,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.creator = creator;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.writer = writer;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(IncomingCurrencySaleSaveRequest request)
        {
            try
            {
                var operationType = await paymentOrderGetter.GetOperationTypeAsync(request.DocumentBaseId);
                if (operationType != OperationType)
                    throw new OperationMismatchTypeExcepton { ActualType = operationType };

                await UpdateOperationAsync(request);
            }
            catch (OperationMismatchTypeExcepton omtex)
            {
                await RecreateOperationAsync(request, omtex.ActualType);
            }

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private async Task RecreateOperationAsync(IncomingCurrencySaleSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(IncomingCurrencySaleSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);
        }
    }
}