using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Business.TaxationSystems;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundToSettlementAccount
{
    [InjectAsSingleton(typeof(IRefundToSettlementAccountUpdater))]
    internal sealed class RefundToSettlementAccountUpdater : IRefundToSettlementAccountUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderIncomingRefundToSettlementAccount;
        private readonly ITaxationSystemTypeReader taxationSystemTypeReader;
        private readonly RefundToSettlementAccountApiClient apiClient;
        private readonly RefundToSettlementAccountEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly RefundToSettlementAccountCustomAccPostingsSaver customAccPostingsSaver;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IRefundToSettlementAccountCreator creator;
        private readonly RefundToSettlementAccountProvideInAccountingFixer provideInAccountingFixer;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public RefundToSettlementAccountUpdater(
            ITaxationSystemTypeReader taxationSystemTypeReader,
            RefundToSettlementAccountApiClient apiClient,
            RefundToSettlementAccountEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            RefundToSettlementAccountCustomAccPostingsSaver customAccPostingsSaver,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IRefundToSettlementAccountCreator creator,
            RefundToSettlementAccountProvideInAccountingFixer provideInAccountingFixer,
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

        public async Task<PaymentOrderSaveResponse> UpdateAsync(RefundToSettlementAccountSaveRequest request)
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

        private async Task RecreateOperationAsync(RefundToSettlementAccountSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(RefundToSettlementAccountSaveRequest request)
        {
            request.TaxationSystemType ??= await taxationSystemTypeReader.GetDefaultByYearAsync(request.Date.Year);
            await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request);

            // кастомные проводки
            await Task.WhenAll(
                customTaxPostingsSaver.OverwriteAsync(MapOverwriteRequest(request)),
                customAccPostingsSaver.OverwriteAsync(request));
        }

        private CustomTaxPostingsOverwriteRequest MapOverwriteRequest(RefundToSettlementAccountSaveRequest request)
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