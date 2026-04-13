using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther
{
    [InjectAsSingleton(typeof(ICurrencyOtherOutgoingUpdater))]
    internal sealed class CurrencyOtherOutgoingUpdater : ICurrencyOtherOutgoingUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingCurrencyOther;
        private readonly CurrencyOtherOutgoingApiClient apiClient;
        private readonly CurrencyOtherOutgoingEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly CurrencyOtherOutgoingCustomAccPostingsSaver customAccPostingsSaver;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly ICurrencyOtherOutgoingCreator creator;
        private readonly CurrencyOtherOutgoingProvideInAccountingFixer provideInAccountingFixer;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public CurrencyOtherOutgoingUpdater(
            CurrencyOtherOutgoingApiClient apiClient,
            CurrencyOtherOutgoingEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            CurrencyOtherOutgoingCustomAccPostingsSaver customAccPostingsSaver,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            ICurrencyOtherOutgoingCreator creator,
            CurrencyOtherOutgoingProvideInAccountingFixer provideInAccountingFixer,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.provideInAccountingFixer = provideInAccountingFixer;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(CurrencyOtherOutgoingSaveRequest request)
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

        private async Task RecreateOperationAsync(CurrencyOtherOutgoingSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(CurrencyOtherOutgoingSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);

            // кастомные проводки
            await Task.WhenAll(
                customTaxPostingsSaver.OverwriteAsync(MapOverwriteRequest(request)),
                customAccPostingsSaver.OverwriteAsync(request));
        }

        private CustomTaxPostingsOverwriteRequest MapOverwriteRequest(CurrencyOtherOutgoingSaveRequest request)
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