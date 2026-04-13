using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    [InjectAsSingleton(typeof(IContributionToAuthorizedCapitalCreator))]
    internal sealed class ContributionToAuthorizedCapitalCreator : IContributionToAuthorizedCapitalCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly ContributionToAuthorizedCapitalApiClient apiClient;
        private readonly ContributionToAuthorizedCapitalEventWriter writer;

        public ContributionToAuthorizedCapitalCreator(
            IBaseDocumentCreator baseDocumentCreator,
            ContributionToAuthorizedCapitalApiClient apiClient,
            ContributionToAuthorizedCapitalEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(ContributionToAuthorizedCapitalSaveRequest request)
        {
            var baseDocumentCreateRequest = ContributionToAuthorizedCapitalMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest).ConfigureAwait(false);

            await apiClient.CreateAsync(request).ConfigureAwait(false);
            await writer.WriteCreatedEventAsync(request).ConfigureAwait(false);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}