using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    [InjectAsSingleton(typeof(IWithdrawalFromAccountReader))]
    internal sealed class WithdrawalFromAccountReader : IWithdrawalFromAccountReader
    {
        private readonly WithdrawalFromAccountApiClient apiClient;
        private readonly WithdrawalFromAccountLinksGetter linksGetter;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public WithdrawalFromAccountReader(
            WithdrawalFromAccountApiClient apiClient,
            WithdrawalFromAccountLinksGetter linksGetter,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.linksGetter = linksGetter;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<WithdrawalFromAccountResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            var links = await linksGetter.GetAsync(documentBaseId).ConfigureAwait(false);
            response.CashOrder = links.CashOrder;
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            return response;
        }
    }
}
