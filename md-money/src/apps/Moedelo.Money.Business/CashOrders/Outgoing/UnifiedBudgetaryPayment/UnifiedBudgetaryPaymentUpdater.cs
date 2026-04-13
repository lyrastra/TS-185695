using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Money.Business.Abstractions.CashOrders;
using Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.CashOrders;
using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentUpdater))]
    internal sealed class UnifiedBudgetaryPaymentUpdater : IUnifiedBudgetaryPaymentUpdater
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly ICashOrderGetter cashOrderGetter;
        private readonly UnifiedBudgetaryPaymentApiClient apiClient;
        private readonly IUnifiedBudgetaryPaymentEventWriter eventWriter;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public UnifiedBudgetaryPaymentUpdater(
            IBaseDocumentCreator baseDocumentCreator,
            ICashOrderGetter cashOrderGetter,
            UnifiedBudgetaryPaymentApiClient apiClient,
            IUnifiedBudgetaryPaymentEventWriter eventWriter,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.cashOrderGetter = cashOrderGetter;
            this.apiClient = apiClient;
            this.eventWriter = eventWriter;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<CashOrderSaveResponse> UpdateAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            await request.SubPayments
                .Where(x => x.DocumentBaseId == 0)
                .RunParallelAsync(subPayment =>
                    CreateSubPaymentBaseIdAsync(request, subPayment));

            var oldOperationType = await cashOrderGetter.GetOperationTypeAsync(request.DocumentBaseId);

            var response = await apiClient.UpdateAsync(request);
            await eventWriter.WriteUpdatedEventAsync(request, oldOperationType, response.DeletedSubPaymentDocumentIds);

            foreach (var subPayment in request.SubPayments)
            {
                await customTaxPostingsSaver.OverwriteAsync(
                    UnifiedBudgetaryPaymentMapper.MapToCustomTaxPostingsOverwriteRequest(request, subPayment));
            }

            return new CashOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private async Task CreateSubPaymentBaseIdAsync(
            UnifiedBudgetaryPaymentSaveRequest request,
            UnifiedBudgetarySubPaymentSaveModel subPayment)
        {
            var baseDocumentCreateRequest = UnifiedBudgetaryPaymentMapper.MapToBaseDocumentCreateRequest(request);
            baseDocumentCreateRequest.Sum = subPayment.Sum;
            subPayment.DocumentBaseId = await baseDocumentCreator.CreateForUnifiedBudgetaryPaymentAsync(baseDocumentCreateRequest);
        }
    }
}