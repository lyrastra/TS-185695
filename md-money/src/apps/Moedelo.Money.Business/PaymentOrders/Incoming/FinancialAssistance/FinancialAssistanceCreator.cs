using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.FinancialAssistance;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Business.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.FinancialAssistance;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.FinancialAssistance
{
    [InjectAsSingleton(typeof(IFinancialAssistanceCreator))]
    internal sealed class FinancialAssistanceCreator : IFinancialAssistanceCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly FinancialAssistanceApiClient apiClient;
        private readonly FinancialAssistanceEventWriter writer;
        private readonly ICustomTaxPostingsSaver customTaxPostingsSaver;

        public FinancialAssistanceCreator(
            IBaseDocumentCreator baseDocumentCreator,
            FinancialAssistanceApiClient apiClient,
            FinancialAssistanceEventWriter writer,
            ICustomTaxPostingsSaver customTaxPostingsSaver)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.customTaxPostingsSaver = customTaxPostingsSaver;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(FinancialAssistanceSaveRequest request)
        {
            var baseDocumentCreateRequest = FinancialAssistanceMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            await customTaxPostingsSaver.OverwriteAsync(
                FinancialAssistanceMapper.MapToCustomTaxPostingsOverwriteRequest(request));

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}