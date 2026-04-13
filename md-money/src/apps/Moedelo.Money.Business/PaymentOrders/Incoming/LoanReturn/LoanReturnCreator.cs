using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanReturn
{
    [InjectAsSingleton(typeof(ILoanReturnCreator))]
    internal sealed class LoanReturnCreator : ILoanReturnCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly LoanReturnApiClient apiClient;
        private readonly LoanReturnEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public LoanReturnCreator(
            IBaseDocumentCreator baseDocumentCreator,
            LoanReturnApiClient apiClient,
            LoanReturnEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(LoanReturnSaveRequest request)
        {
            var baseDocumentCreateRequest = LoanReturnMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                LoanReturnMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}