using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(UnifiedBudgetaryPaymentImportValidator))]
    internal sealed class UnifiedBudgetaryPaymentImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;


        public UnifiedBudgetaryPaymentImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
        }

        public async Task ValidateAsync(UnifiedBudgetaryPaymentImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
        }
    }
}