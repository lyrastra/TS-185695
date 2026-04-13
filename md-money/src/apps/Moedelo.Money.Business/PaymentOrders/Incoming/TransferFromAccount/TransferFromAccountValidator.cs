using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromAccount
{
    [InjectAsSingleton(typeof(ITransferFromAccountValidator))]
    internal sealed class TransferFromAccountValidator : ITransferFromAccountValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;

        public TransferFromAccountValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
        }

        public async Task ValidateAsync(TransferFromAccountSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateAsync(request.FromSettlementAccountId);
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
        }

        public async Task ValidateAsync(TransferFromAccountShortSaveRequest request)
        {
            await closedPeriodValidator.ValidateAsync(request.Date);
        }
    }
}
