using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanObtaining
{
    [InjectAsSingleton(typeof(ILoanObtainingReader))]
    internal sealed class LoanObtainingReader : ILoanObtainingReader
    {
        private readonly LoanObtainingApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly LoanObtainingLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public LoanObtainingReader(
            LoanObtainingApiClient apiClient,
            IKontragentsReader kontragentsReader,
            LoanObtainingLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<LoanObtainingResponse> GetByBaseIdAsync(long baseDaseId)
        {
            var response = await apiClient.GetAsync(baseDaseId).ConfigureAwait(false);
            if (response.Kontragent != null)
            {
                var kontraget = await kontragentsReader.GetByIdAsync(response.Kontragent.Id).ConfigureAwait(false);
                response.Kontragent.Form = (int?)kontraget?.Form;
            }
            var links = await linksGetter.GetAsync(baseDaseId).ConfigureAwait(false);
            response.Contract = links.Contract;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
