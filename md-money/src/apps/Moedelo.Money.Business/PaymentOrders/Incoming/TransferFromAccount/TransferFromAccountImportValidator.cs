using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount
{
    [InjectAsSingleton(typeof(TransferFromAccountImportValidator))]
    internal sealed class TransferFromAccountImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;

        public TransferFromAccountImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
        }

        public async Task ValidateAsync(TransferFromAccountImportRequest request)
        {
            await settlementAccountsValidator.ValidateAsync(request.FromSettlementAccountId);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
        }
    }
}
