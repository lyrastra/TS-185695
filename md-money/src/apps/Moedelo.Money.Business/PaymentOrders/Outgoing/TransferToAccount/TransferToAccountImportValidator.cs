using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton(typeof(TransferToAccountImportValidator))]
    internal sealed class TransferToAccountImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;

        public TransferToAccountImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
        }

        public async Task ValidateAsync(TransferToAccountImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateAsync(request.ToSettlementAccountId).ConfigureAwait(false);
        }
    }
}