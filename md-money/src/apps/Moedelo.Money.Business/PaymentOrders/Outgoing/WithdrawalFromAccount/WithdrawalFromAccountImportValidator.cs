using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    [InjectAsSingleton(typeof(WithdrawalFromAccountImportValidator))]
    internal sealed class WithdrawalFromAccountImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ICashOrdersValidator cashOrdersValidator;

        public WithdrawalFromAccountImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            ICashOrdersValidator cashOrdersValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.cashOrdersValidator = cashOrdersValidator;
        }

        public async Task ValidateAsync(WithdrawalFromAccountImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            if (request.CashOrderBaseId.HasValue)
            {
                await cashOrdersValidator.ValidateAsync(request.CashOrderBaseId.Value,
                    OperationType.CashOrderIncomingFromSettlementAccount,
                    MoneyDirection.Incoming).ConfigureAwait(false);
            }
        }
    }
}