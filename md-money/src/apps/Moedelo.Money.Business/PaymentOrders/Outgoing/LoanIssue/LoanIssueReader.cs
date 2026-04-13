using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanIssue
{
    [InjectAsSingleton(typeof(ILoanIssueReader))]
    internal sealed class LoanIssueReader : ILoanIssueReader
    {
        private readonly LoanIssueApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly LoanIssueLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public LoanIssueReader(
            LoanIssueApiClient apiClient,
            IKontragentsReader kontragentsReader,
            LoanIssueLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<LoanIssueResponse> GetByBaseIdAsync(long baseDaseId)
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
