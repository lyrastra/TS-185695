using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.AgencyContract
{
    [InjectAsSingleton(typeof(IAgencyContractReader))]
    class AgencyContractReader : IAgencyContractReader
    {
        private readonly AgencyContractApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly AgencyContractLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public AgencyContractReader(
            AgencyContractApiClient apiClient,
            IKontragentsReader kontragentsReader,
            AgencyContractLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<AgencyContractResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            if (response.Kontragent != null)
            {
                var kontraget = await kontragentsReader.GetByIdAsync(response.Kontragent.Id).ConfigureAwait(false);
                response.Kontragent.Form = (int?)kontraget?.Form;
            }
            var documents = await linksGetter.GetAsync(documentBaseId).ConfigureAwait(false);
            response.Contract = documents.Contract;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
