using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer.Links;
using Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.MediationFee
{
    [InjectAsSingleton(typeof(IMediationFeeReader))]
    internal sealed class MediationFeeReader : IMediationFeeReader
    {
        private readonly MediationFeeApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly MediationFeeLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public MediationFeeReader(
            MediationFeeApiClient apiClient,
            IKontragentsReader kontragentsReader,
            MediationFeeLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<MediationFeeResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            if (response.Kontragent != null)
            {
                var kontraget = await kontragentsReader.GetByIdAsync(response.Kontragent.Id).ConfigureAwait(false);
                response.Kontragent.Form = (int?)kontraget?.Form;
            }
            var documents = await linksGetter.GetAsync(documentBaseId).ConfigureAwait(false);
            response.Bills = documents.Bills;
            response.Contract = documents.Contract;
            response.Documents = documents.Documents;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
