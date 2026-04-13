using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyOther
{
    [InjectAsSingleton(typeof(ICurrencyOtherIncomingUpdater))]
    internal sealed class CurrencyOtherIncomingUpdater : ICurrencyOtherIncomingUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderIncomingCurrencyOther;
        private readonly CurrencyOtherIncomingApiClient apiClient;
        private readonly CurrencyOtherIncomingEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly CurrencyOtherIncomingCustomAccPostingsSaver customAccPostingsSaver;
        private readonly IPaymentOrderRemover remover;
        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly ICurrencyOtherIncomingCreator creator;
        private readonly CurrencyOtherIncomingProvideInAccountingFixer provideInAccountingFixer;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public CurrencyOtherIncomingUpdater(
            CurrencyOtherIncomingApiClient apiClient,
            CurrencyOtherIncomingEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            CurrencyOtherIncomingCustomAccPostingsSaver customAccPostingsSaver,
            IPaymentOrderRemover remover,
            IPaymentOrderGetter paymentOrderGetter,
            ICurrencyOtherIncomingCreator creator,
            CurrencyOtherIncomingProvideInAccountingFixer provideInAccountingFixer,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.customAccPostingsSaver = customAccPostingsSaver;
            this.remover = remover;
            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.provideInAccountingFixer = provideInAccountingFixer;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(CurrencyOtherIncomingSaveRequest request)
        {
            await provideInAccountingFixer.FixAsync(request);

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

        private async Task RecreateOperationAsync(CurrencyOtherIncomingSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(CurrencyOtherIncomingSaveRequest request)
        {
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);

            // кастомные проводки
            await Task.WhenAll(
                customTaxPostingsSaver.OverwriteAsync(MapOverwriteRequest(request)),
                customAccPostingsSaver.OverwriteAsync(request));
        }

        private static CustomTaxPostingsOverwriteRequest MapOverwriteRequest(CurrencyOtherIncomingSaveRequest request)
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