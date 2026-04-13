using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton(typeof(IRentPaymentReader))]
    internal sealed class RentPaymentReader : IRentPaymentReader
    {
        private readonly RentPaymentApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;
        private readonly RentPaymentLinksGetter linksGetter;
        private readonly RentPeriodsReader rentPeriodsReader;

        public RentPaymentReader(
            RentPaymentApiClient apiClient,
            IKontragentsReader kontragentsReader,
            IPaymentOrderAccessor paymentOrderAccessor,
            RentPaymentLinksGetter linksGetter,
            RentPeriodsReader rentPeriodsReader)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.paymentOrderAccessor = paymentOrderAccessor;
            this.linksGetter = linksGetter;
            this.rentPeriodsReader = rentPeriodsReader;
        }

        public async Task<RentPaymentResponse> GetByBaseIdAsync(long baseId)
        {
            var response = await apiClient.GetAsync(baseId);
            var documents = await linksGetter.GetAsync(baseId);
            response.Contract = documents.Contract;
            response.InventoryCard = documents.InventoryCard;

            var kontragentTask = response.Kontragent?.Id > 0
                ? kontragentsReader.GetByIdAsync(response.Kontragent.Id)
                : Task.FromResult<Kontragent>(null);

            var rentPeriodsTask = rentPeriodsReader.GetAsync(response.RentPeriods);

            await Task.WhenAll(kontragentTask, rentPeriodsTask);

            response.Kontragent.Form = (int?)kontragentTask.Result?.Form;
            response.RentPeriods = rentPeriodsTask.Result;

            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }

    }
}
