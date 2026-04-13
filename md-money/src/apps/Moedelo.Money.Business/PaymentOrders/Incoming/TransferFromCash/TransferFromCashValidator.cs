using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(ITransferFromCashValidator))]
    internal sealed class TransferFromCashValidator : ITransferFromCashValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ICashOrdersValidator cashOrdersValidator;

        public TransferFromCashValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            ICashOrdersValidator cashOrdersValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.cashOrdersValidator = cashOrdersValidator;
        }

        public async Task ValidateAsync(TransferFromCashSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);
            if (request.CashOrderBaseId.HasValue)
            {
                await cashOrdersValidator.ValidateAsync(request.CashOrderBaseId.Value,
                    OperationType.CashOrderOutcomingToSettlementAccount,
                    MoneyDirection.Outgoing).ConfigureAwait(false);
            }
        }
    }
}
