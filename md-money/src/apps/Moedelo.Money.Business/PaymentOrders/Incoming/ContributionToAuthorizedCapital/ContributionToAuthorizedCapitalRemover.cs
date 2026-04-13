using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    [OperationType(OperationType.PaymentOrderIncomingContributionToAuthorizedCapital)]
    [InjectAsSingleton(typeof(IContributionToAuthorizedCapitalRemover))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderRemover))]
    internal sealed class ContributionToAuthorizedCapitalRemover : IContributionToAuthorizedCapitalRemover, IConcretePaymentOrderRemover
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ContributionToAuthorizedCapitalApiClient apiClient;
        private readonly ContributionToAuthorizedCapitalEventWriter writer;

        public ContributionToAuthorizedCapitalRemover(
            IClosedPeriodValidator closedPeriodValidator,
            ContributionToAuthorizedCapitalApiClient apiClient,
            ContributionToAuthorizedCapitalEventWriter writer)
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