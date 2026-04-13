using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    [OperationType(OperationType.PaymentOrderIncomingContributionOfOwnFunds)]
    [InjectAsSingleton(typeof(IContributionOfOwnFundsRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class ContributionOfOwnFundsRemover : IContributionOfOwnFundsRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ContributionOfOwnFundsApiClient apiClient;
        private readonly ContributionOfOwnFundsEventWriter writer;

        public ContributionOfOwnFundsRemover(
            IClosedPeriodValidator closedPeriodValidator,
            ContributionOfOwnFundsApiClient apiClient,
            ContributionOfOwnFundsEventWriter writer)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.apiClient = apiClient;
            this.writer = writer;

        }

        public async Task DeleteAsync(long documentBaseId, long? newDocumentBaseId = null)
        {
            var paymentOrder = await apiClient.GetAsync(documentBaseId);
            await closedPeriodValidator.ValidateAsync(paymentOrder.Date);
            await apiClient.DeleteAsync(documentBaseId);
            await writer.WriteDeletedEventAsync(paymentOrder, newDocumentBaseId);
        }
    }
}