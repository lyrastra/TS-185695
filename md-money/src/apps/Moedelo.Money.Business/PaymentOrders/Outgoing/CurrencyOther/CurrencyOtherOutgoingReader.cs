using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Konragents.Enums;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Business.FirmRequisites;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther
{
    [InjectAsSingleton(typeof(ICurrencyOtherOutgoingReader))]
    internal sealed class CurrencyOtherOutgoingReader : ICurrencyOtherOutgoingReader
    {
        private readonly CurrencyOtherOutgoingApiClient apiClient;
        private readonly CurrencyOtherOutgoingLinksGetter linksGetter;
        private readonly IKontragentsReader kontragentsReader;
        private readonly IFirmRequisitesReader firmRequisitesReader;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public CurrencyOtherOutgoingReader(
            CurrencyOtherOutgoingApiClient apiClient,
            CurrencyOtherOutgoingLinksGetter linksGetter,
            IKontragentsReader kontragentsReader,
            IFirmRequisitesReader firmRequisitesReader,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.linksGetter = linksGetter;
            this.kontragentsReader = kontragentsReader;
            this.firmRequisitesReader = firmRequisitesReader;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<CurrencyOtherOutgoingResponse> GetByBaseIdAsync(long baseId)
        {
            var response = await apiClient.GetAsync(baseId).ConfigureAwait(false);
            
            // костыль по аналогии с другими операциями
            if (response.Contactor != null)
            {
                if (response.Contactor.Id == 0)
                {
                    var isOoo = await firmRequisitesReader.IsOooAsync();
                    response.Contactor.Form = (int?) (isOoo ? KontragentForm.UL : KontragentForm.IP);
                }
                else
                {
                    var kontraget = await kontragentsReader.GetByIdAsync(response.Contactor.Id);
                    response.Contactor.Form = (int?) (kontraget?.Form ?? KontragentForm.IP);
                }
            }
            
            var documents = await linksGetter.GetAsync(baseId);
            response.Contract = documents.Contract;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}