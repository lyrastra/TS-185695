using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.CashOrders;
using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentCreator))]
    class UnifiedBudgetaryPaymentCreator : IUnifiedBudgetaryPaymentCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly UnifiedBudgetaryPaymentApiClient apiClient;
        private readonly IUnifiedBudgetaryPaymentEventWriter eventWriter;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public UnifiedBudgetaryPaymentCreator(
            IBaseDocumentCreator baseDocumentCreator,
            UnifiedBudgetaryPaymentApiClient apiClient,
            IUnifiedBudgetaryPaymentEventWriter eventWriter,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.eventWriter = eventWriter;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<CashOrderSaveResponse> CreateAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            var baseDocumentCreateRequest = UnifiedBudgetaryPaymentMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForOutgoingCashOrderAsync(baseDocumentCreateRequest);

            await request.SubPayments.RunParallelAsync(subPayment =>
                CreateSubPaymentBaseIdAsync(request, subPayment));

            await apiClient.CreateAsync(request);
            await eventWriter.WriteCreatedEventAsync(request);

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