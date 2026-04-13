using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    [InjectAsSingleton(typeof(IWithdrawalOfProfitReader))]
    internal class WithdrawalOfProfitReader : IWithdrawalOfProfitReader
    {
        private readonly WithdrawalOfProfitApiClient apiClient;
        private readonly IKontragentsReader kontragentsReader;
        private readonly IPaymentOrderAccessor paymentOrderAccessor;

        public WithdrawalOfProfitReader(
            WithdrawalOfProfitApiClient apiClient,
            IKontragentsReader kontragentsReader,
            IPaymentOrderAccessor paymentOrderAccessor)
        {
            this.apiClient = apiClient;
            this.kontragentsReader = kontragentsReader;
            this.paymentOrderAccessor = paymentOrderAccessor;
        }

        public async Task<WithdrawalOfProfitResponse> GetByBaseIdAsync(long documentBaseId)
        {
            var response = await apiClient.GetAsync(documentBaseId).ConfigureAwait(false);
            response.IsReadOnly = paymentOrderAccessor.IsReadOnly(response);
            if (response.Kontragent != null)
            {
                var kontraget = await kontragentsReader.GetByIdAsync(response.Kontragent.Id).ConfigureAwait(false);
                response.Kontragent.Form = (int?)kontraget?.Form;
            }
            return response;
        }
    }
}
