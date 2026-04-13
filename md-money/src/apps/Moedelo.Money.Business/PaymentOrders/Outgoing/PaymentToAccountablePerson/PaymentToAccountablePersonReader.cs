using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Domain.Enums;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(IPaymentToAccountablePersonReader))]
    class PaymentToAccountablePersonReader : IPaymentToAccountablePersonReader
    {
        private readonly PaymentToAccountablePersonApiClient apiClient;
        private readonly PaymentToAccountablePersonLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public PaymentToAccountablePersonReader(
            PaymentToAccountablePersonApiClient apiClient,
            PaymentToAccountablePersonLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<PaymentToAccountablePersonResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            var documents = await linksGetter.GetAsync(documentBaseId).ConfigureAwait(false);
            response.AdvanceStatements = documents.Documents;
            // hack: при сохранении авансового отчета для п/п создаются связанные проводки,
            // но статус остается "проведен вручную", поэтому при пересохранении проводки слетают
            // будем считать, что при наличии авансового отчета не может быть кастомных проводок
            // пока не знаю как лучше разрулить этот костыль
            if (response.AdvanceStatements.Status == RemoteServiceStatus.Ok &&
                response.AdvanceStatements.Data != null &&
                response.AdvanceStatements.Data.Count > 0)
            {
                response.TaxPostingsInManualMode = false;
            }
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
