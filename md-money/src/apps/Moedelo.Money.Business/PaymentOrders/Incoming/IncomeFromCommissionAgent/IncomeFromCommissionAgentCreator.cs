using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    [InjectAsSingleton(typeof(IIncomeFromCommissionAgentCreator))]
    class IncomeFromCommissionAgentCreator : IIncomeFromCommissionAgentCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly IncomeFromCommissionAgentApiClient apiClient;
        private readonly IncomeFromCommissionAgentEventWriter writer;

        public IncomeFromCommissionAgentCreator(
            IBaseDocumentCreator baseDocumentCreator,
            IncomeFromCommissionAgentApiClient apiClient,
            IncomeFromCommissionAgentEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(IncomeFromCommissionAgentSaveRequest request)
        {
            var baseDocumentCreateRequest = IncomeFromCommissionAgentMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}