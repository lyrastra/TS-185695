using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionOfOwnFunds;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    [InjectAsSingleton(typeof(IContributionOfOwnFundsReader))]
    internal sealed class ContributionOfOwnFundsReader : IContributionOfOwnFundsReader
    {
        private readonly ContributionOfOwnFundsApiClient apiClient;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public ContributionOfOwnFundsReader(
            ContributionOfOwnFundsApiClient apiClient,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<ContributionOfOwnFundsResponse> GetByBaseIdAsync(long baseId)
        {
            var response = await apiClient.GetAsync(baseId).ConfigureAwait(false);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
