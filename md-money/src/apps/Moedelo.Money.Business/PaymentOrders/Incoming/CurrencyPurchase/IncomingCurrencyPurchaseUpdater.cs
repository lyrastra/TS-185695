using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IIncomingCurrencyPurchaseUpdater))]
    internal sealed class IncomingCurrencyPurchaseUpdater : IIncomingCurrencyPurchaseUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderIncomingCurrencyPurchase;
        private readonly IncomingCurrencyPurchaseApiClient apiClient;
        private readonly IIncomingCurrencyPurchaseCreator creator;
        private readonly IPaymentOrderRemover remover;
        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IncomingCurrencyPurchaseEventWriter writer;
        private readonly IncomingCurrencyPurchaseToDtoMapper dtoMapper;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public IncomingCurrencyPurchaseUpdater(
            IncomingCurrencyPurchaseApiClient apiClient,
            IIncomingCurrencyPurchaseCreator creator,
            IPaymentOrderRemover remover,
            IPaymentOrderGetter paymentOrderGetter,
            IncomingCurrencyPurchaseEventWriter writer,
            IncomingCurrencyPurchaseToDtoMapper dtoMapper,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.creator = creator;
            this.remover = remover;
            this.paymentOrderGetter = paymentOrderGetter;
            this.writer = writer;
            this.dtoMapper = dtoMapper;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(IncomingCurrencyPurchaseSaveRequest request)
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

        private async Task RecreateOperationAsync(IncomingCurrencyPurchaseSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(IncomingCurrencyPurchaseSaveRequest request)
        {
            var dto = await dtoMapper.MapAsync(request);
            await apiClient.UpdateAsync(dto);
            await writer.WriteUpdatedEventAsync(request);
        }
    }
}