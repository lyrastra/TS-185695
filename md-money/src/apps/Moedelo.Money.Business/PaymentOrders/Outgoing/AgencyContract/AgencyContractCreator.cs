using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.AgencyContract
{
    [InjectAsSingleton(typeof(IAgencyContractCreator))]
    class AgencyContractCreator : IAgencyContractCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly AgencyContractApiClient apiClient;
        private readonly AgencyContractEventWriter writer;

        public AgencyContractCreator(
            IBaseDocumentCreator baseDocumentCreator,
            AgencyContractApiClient apiClient,
            AgencyContractEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(AgencyContractSaveRequest request)
        {
            var baseDocumentCreateRequest = AgencyContractMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest);

            await apiClient.CreateAsync(request);
            await writer.WriteCreatedEventAsync(request);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}