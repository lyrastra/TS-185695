using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(TransferFromCashImportValidator))]
    internal sealed class TransferFromCashImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ICashOrdersValidator cashOrdersValidator;

        public TransferFromCashImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            ICashOrdersValidator cashOrdersValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.cashOrdersValidator = cashOrdersValidator;
        }

        public async Task ValidateAsync(TransferFromCashImportRequest request)
        {
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
