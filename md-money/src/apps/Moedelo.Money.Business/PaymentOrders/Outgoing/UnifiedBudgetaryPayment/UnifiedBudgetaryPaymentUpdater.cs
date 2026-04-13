using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentUpdater))]
    internal sealed class UnifiedBudgetaryPaymentUpdater : IUnifiedBudgetaryPaymentUpdater
    {
        private readonly OperationType OperationType = OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment;
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly IUnifiedBudgetaryPaymentApiClient apiClient;
        private readonly IUnifiedBudgetaryPaymentEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;
        private readonly IPaymentOrderRemover remover;        private readonly IPaymentOrderGetter paymentOrderGetter;
        private readonly IUnifiedBudgetaryPaymentCreator creator;
        private readonly PaymentOrderOperationEventWriter operationEventWriter;

        public UnifiedBudgetaryPaymentUpdater(
            IBaseDocumentCreator baseDocumentCreator,
            IUnifiedBudgetaryPaymentApiClient apiClient,
            IUnifiedBudgetaryPaymentEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver,
            IPaymentOrderRemover remover,            IPaymentOrderGetter paymentOrderGetter,
            IUnifiedBudgetaryPaymentCreator creator,
            PaymentOrderOperationEventWriter operationEventWriter)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.remover = remover;            this.paymentOrderGetter = paymentOrderGetter;
            this.creator = creator;
            this.operationEventWriter = operationEventWriter;
        }

        public async Task<PaymentOrderSaveResponse> UpdateAsync(UnifiedBudgetaryPaymentSaveRequest request)
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

        private async Task RecreateOperationAsync(UnifiedBudgetaryPaymentSaveRequest request, OperationType oldOperationType)
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

        private async Task UpdateOperationAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            await request.SubPayments
                .Where(x => x.DocumentBaseId == 0)
                .RunParallelAsync(subPayment =>
                    CreateSubPaymentBaseIdAsync(request, subPayment));

            var response = await apiClient.UpdateAsync(request);
            await writer.WriteUpdatedEventAsync(request, response.DeletedSubPaymentDocumentIds);

            foreach (var subPayment in request.SubPayments)
            {
                await customTaxPostingsSaver.OverwriteAsync(new CustomTaxPostingsOverwriteRequest
                {
                    DocumentBaseId = subPayment.DocumentBaseId,
                    DocumentDate = request.Date,
                    DocumentNumber = request.Number,
                    Description = request.Description,
                    TaxationSystemType = null,
                    Postings = subPayment.TaxPostings
                });
            }
        }

        private async Task CreateSubPaymentBaseIdAsync(
        UnifiedBudgetaryPaymentSaveRequest request,
        UnifiedBudgetarySubPaymentSaveModel subPayment)
        {
            var baseDocumentCreateRequest = new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = subPayment.Sum
            };
            subPayment.DocumentBaseId = await baseDocumentCreator.CreateForUnifiedBudgetaryPaymentAsync(baseDocumentCreateRequest);
        }
    }
}