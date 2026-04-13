using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanIssue
{
    [InjectAsSingleton(typeof(ILoanIssueCreator))]
    internal sealed class LoanIssueCreator : ILoanIssueCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly LoanIssueApiClient apiClient;
        private readonly LoanIssueEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public LoanIssueCreator(
            IBaseDocumentCreator baseDocumentCreator,
            LoanIssueApiClient apiClient,
            LoanIssueEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(LoanIssueSaveRequest request)
        {
            var baseDocumentCreateRequest = LoanIssueMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                LoanIssueMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}