using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale
{
    [InjectAsSingleton(typeof(IOutgoingCurrencySaleUpdater))]
    internal sealed class OutgoingCurrencySaleUpdater : IOutgoingCurrencySaleUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingCurrencySale;
        private readonly OutgoingCurrencySaleApiClient apiClient;
        private readonly IOutgoingCurrencySaleCreator creator;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly OutgoingCurrencySaleEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public OutgoingCurrencySaleUpdater(
            OutgoingCurrencySaleApiClient apiClient,
            IOutgoingCurrencySaleCreator creator,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            OutgoingCurrencySaleEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.creator = creator;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(OutgoingCurrencySaleSaveRequest request)
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

        private async Task RecreateOperationAsync(OutgoingCurrencySaleSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(OutgoingCurrencySaleSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                OutgoingCurrencySaleMapper.MapToCustomTaxPostingsOverwriteRequest(request));
        }
    }
}