using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    [InjectAsSingleton(typeof(IOtherOutgoingReader))]
    internal sealed class OtherOutgoingReader : IOtherOutgoingReader
    {
        private readonly OtherOutgoingApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly OtherOutgoingLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public OtherOutgoingReader(
            OtherOutgoingApiClient apiClient,
            IKontragentsReader kontragentsReader,
            OtherOutgoingLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<OtherOutgoingResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            if (response.Contractor?.Type == ContractorType.Kontragent)
            {
                var kontraget = await kontragentsReader.GetByIdAsync(response.Contractor.Id).ConfigureAwait(false);
                response.Contractor.Form = (int?)kontraget?.Form;
            }
            var documents = await linksGetter.GetAsync(documentBaseId).ConfigureAwait(false);
            response.Contract = documents.Contract;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
