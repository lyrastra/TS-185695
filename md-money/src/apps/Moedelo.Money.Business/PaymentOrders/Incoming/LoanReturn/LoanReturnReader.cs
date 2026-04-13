using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanReturn
{
    [InjectAsSingleton(typeof(ILoanReturnReader))]
    internal sealed class LoanReturnReader : ILoanReturnReader
    {
        private readonly LoanReturnApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly LoanReturnLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public LoanReturnReader(
            LoanReturnApiClient apiClient,
            IKontragentsReader kontragentsReader,
            LoanReturnLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<LoanReturnResponse> GetByBaseIdAsync(long baseDaseId)
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
