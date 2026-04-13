using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    [InjectAsSingleton(typeof(IOtherOutgoingUpdater))]
    internal sealed class OtherOutgoingUpdater : IOtherOutgoingUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingOther;
        private readonly OtherOutgoingApiClient apiClient;
        private readonly OtherOutgoingEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly OtherOutgoingCustomAccPostingsSaver customAccPostingsSaver;
        private readonly OtherOutgoingProvideInAccountingFixer provideInAccountingFixer;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IOtherOutgoingCreator creator;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public OtherOutgoingUpdater(
            OtherOutgoingApiClient apiClient,
            OtherOutgoingEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            OtherOutgoingCustomAccPostingsSaver customAccPostingsSaver,
            OtherOutgoingProvideInAccountingFixer provideInAccountingFixer,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IOtherOutgoingCreator creator,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.provideInAccountingFixer = provideInAccountingFixer;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(OtherOutgoingSaveRequest request)
        {
            await provideInAccountingFixer.FixAsync(request);

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

        private async Task RecreateOperationAsync(OtherOutgoingSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(OtherOutgoingSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);

            await ProvideCustomPostingsAsync(request);
        }

        private async Task ProvideCustomPostingsAsync(OtherOutgoingSaveRequest request)
        {
            if (request.IsPaid == false)
            {
                return;
            }

            // кастомные проводки
            await Task.WhenAll(
                customTaxPostingsSaver.OverwriteAsync(MapOverwriteRequest(request)),
                customAccPostingsSaver.OverwriteAsync(request));
        }

        private CustomTaxPostingsOverwriteRequest MapOverwriteRequest(OtherOutgoingSaveRequest request)
        {
            return new CustomTaxPostingsOverwriteRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                DocumentDate = request.Date,
                DocumentNumber = request.Number,
                Description = request.Description,
                TaxationSystemType = null,
                Postings = request.TaxPostings
            };
        }
    }
}