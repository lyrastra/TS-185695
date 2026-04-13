using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment
{
    [InjectAsSingleton(typeof(ILoanRepaymentCreator))]
    internal sealed class LoanObtainingCreator : ILoanRepaymentCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly LoanRepaymentApiClient apiClient;
        private readonly LoanRepaymentEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public LoanObtainingCreator(
            IBaseDocumentCreator baseDocumentCreator,
            LoanRepaymentApiClient apiClient,
            LoanRepaymentEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(LoanRepaymentSaveRequest request)
        {
            var baseDocumentCreateRequest = LoanRepaymentMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                LoanRepaymentMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}