using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment
{
    [InjectAsSingleton(typeof(ILoanRepaymentReader))]
    internal sealed class LoanRepaymentReader : ILoanRepaymentReader
    {
        private readonly LoanRepaymentApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly LoanRepaymentLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public LoanRepaymentReader(
            LoanRepaymentApiClient apiClient,
            IKontragentsReader kontragentsReader,
            LoanRepaymentLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<LoanRepaymentResponse> GetByBaseIdAsync(long baseDaseId)
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
