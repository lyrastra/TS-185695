using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.System.Extensions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.TaxPostings;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentCreator))]
    class UnifiedBudgetaryPaymentCreator : IUnifiedBudgetaryPaymentCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly IUnifiedBudgetaryPaymentApiClient apiClient;
        private readonly IUnifiedBudgetaryPaymentEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public UnifiedBudgetaryPaymentCreator(
            IBaseDocumentCreator baseDocumentCreator,
            IUnifiedBudgetaryPaymentApiClient apiClient,
            IUnifiedBudgetaryPaymentEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            await FillSaveRequestAsync(request);
            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);
            foreach(var subPayment in request.SubPayments)
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
            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }

        private async Task FillSaveRequestAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            var baseDocumentCreateRequest = new BaseDocumentCreateRequest
            {
                Number = request.Number,
                Date = request.Date,
                Sum = request.Sum
            };
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await request.SubPayments.RunParallelAsync(subPayment => CreateSubPaymentBaseIdAsync(baseDocumentCreateRequest, subPayment));
        }

        private async Task CreateSubPaymentBaseIdAsync(BaseDocumentCreateRequest request, UnifiedBudgetarySubPaymentSaveModel subPayment)
        {
            request.Sum = subPayment.Sum;
            subPayment.DocumentBaseId = await baseDocumentCreator.CreateForUnifiedBudgetaryPaymentAsync(request);
        }
    }
}