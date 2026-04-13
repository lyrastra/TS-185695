using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentPayerKppSetter))]
    class BudgetaryPaymentPayerKppSetter : IBudgetaryPaymentPayerKppSetter
    {
        private readonly BudgetaryPaymentApiClient apiClient;

        public BudgetaryPaymentPayerKppSetter(
            BudgetaryPaymentApiClient apiClient)
        {
            this.apiClient = apiClient;
        }
        
        public Task SetPayerKppAsync(long documentBaseId, string kpp)
        {
            return apiClient.SetPayerKppAsync(documentBaseId, kpp);
        }
    }
}
