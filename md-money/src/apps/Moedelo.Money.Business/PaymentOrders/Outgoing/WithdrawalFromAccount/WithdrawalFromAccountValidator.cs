using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    [InjectAsSingleton(typeof(IWithdrawalFromAccountValidator))]
    internal sealed class WithdrawalFromAccountValidator : IWithdrawalFromAccountValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ICashOrdersValidator cashOrdersValidator;

        public WithdrawalFromAccountValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            ICashOrdersValidator cashOrdersValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.cashOrdersValidator = cashOrdersValidator;
        }

        public async Task ValidateAsync(WithdrawalFromAccountSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);
            if (request.CashOrderBaseId.HasValue)
            {
                await cashOrdersValidator.ValidateAsync(request.CashOrderBaseId.Value,
                    OperationType.CashOrderIncomingFromSettlementAccount,
                    MoneyDirection.Incoming).ConfigureAwait(false);
            }
        }
    }
}