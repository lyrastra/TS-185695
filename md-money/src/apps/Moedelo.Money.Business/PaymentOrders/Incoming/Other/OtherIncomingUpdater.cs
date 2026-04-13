using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.Other;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.Other;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other
{
    [InjectAsSingleton(typeof(IOtherIncomingUpdater))]
    internal sealed class OtherIncomingUpdater : IOtherIncomingUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderIncomingOther;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly OtherIncomingApiClient apiClient;
        private readonly OtherIncomingEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly OtherIncomingCustomAccPostingsSaver customAccPostingsSaver;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IOtherIncomingCreator creator;
        private readonly OtherIncomingProvideInAccountingFixer provideInAccountingFixer;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public OtherIncomingUpdater(
            ITaxationSystemTypeReader taxationSystemTypeReader,
            OtherIncomingApiClient apiClient,
            OtherIncomingEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            OtherIncomingCustomAccPostingsSaver customAccPostingsSaver,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IOtherIncomingCreator creator,
            OtherIncomingProvideInAccountingFixer provideInAccountingFixer,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.taxationSystemTypeReader = taxationSystemTypeReader;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.provideInAccountingFixer = provideInAccountingFixer;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(OtherIncomingSaveRequest request)
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

        private async Task RecreateOperationAsync(OtherIncomingSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(OtherIncomingSaveRequest request)
        {
            request.TaxationSystemType ??= await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);

            // кастомные проводки
            await Task.WhenAll(
                customTaxPostingsSaver.OverwriteAsync(MapOverwriteRequest(request)),
                customAccPostingsSaver.OverwriteAsync(request));
        }

        private CustomTaxPostingsOverwriteRequest MapOverwriteRequest(OtherIncomingSaveRequest request)
        {
            return new CustomTaxPostingsOverwriteRequest
            {
                DocumentBaseId = request.DocumentBaseId,
                DocumentDate = request.Date,
                DocumentNumber = request.Number,
                Description = request.Description,
                TaxationSystemType = request.TaxationSystemType,
                Postings = request.TaxPostings
            };
        }
    }
}