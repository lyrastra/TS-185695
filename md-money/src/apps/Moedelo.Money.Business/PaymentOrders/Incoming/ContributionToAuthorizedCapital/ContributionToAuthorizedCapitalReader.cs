using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    [InjectAsSingleton(typeof(IContributionToAuthorizedCapitalReader))]
    internal sealed class ContributionToAuthorizedCapitalReader : IContributionToAuthorizedCapitalReader
    {
        private readonly ContributionToAuthorizedCapitalApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public ContributionToAuthorizedCapitalReader(
            ContributionToAuthorizedCapitalApiClient apiClient,
            IKontragentsReader kontragentsReader,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<ContributionToAuthorizedCapitalResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            if (response.Kontragent != null)
            {
                var kontraget = await kontragentsReader.GetByIdAsync(response.Kontragent.Id).ConfigureAwait(false);
                response.Kontragent.Form = (int?)kontraget?.Form;
            }
            return response;
        }
    }
}
