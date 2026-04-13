using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Business.LinkedDocuments.BaseDocuments;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    [InjectAsSingleton(typeof(IContributionOfOwnFundsCreator))]
    internal sealed class ContributionOfOwnFundsCreator : IContributionOfOwnFundsCreator
    {
        private readonly IBaseDocumentCreator baseDocumentCreator;
        private readonly ContributionOfOwnFundsApiClient apiClient;
        private readonly ContributionOfOwnFundsEventWriter writer;

        public ContributionOfOwnFundsCreator(
            IBaseDocumentCreator baseDocumentCreator,
            ContributionOfOwnFundsApiClient apiClient,
            ContributionOfOwnFundsEventWriter writer)
        {
            this.baseDocumentCreator = baseDocumentCreator;
            this.apiClient = apiClient;
            this.writer = writer;
        }

        public async Task<PaymentOrderSaveResponse> CreateAsync(ContributionOfOwnFundsSaveRequest request)
        {
            var baseDocumentCreateRequest = ContributionOfOwnFundsMapper.MapToBaseDocumentCreateRequest(request);
            request.DocumentBaseId = await baseDocumentCreator.CreateForPaymentOrderAsync(baseDocumentCreateRequest).ConfigureAwait(false);

            await apiClient.CreateAsync(request).ConfigureAwait(false);
            await writer.WriteCreatedEventAsync(request).ConfigureAwait(false);

            return new PaymentOrderSaveResponse { DocumentBaseId = request.DocumentBaseId };
        }
    }
}