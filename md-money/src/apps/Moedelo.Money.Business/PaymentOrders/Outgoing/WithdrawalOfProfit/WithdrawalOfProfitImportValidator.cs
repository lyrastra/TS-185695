using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    [InjectAsSingleton(typeof(WithdrawalOfProfitImportValidator))]
    internal sealed class WithdrawalOfProfitImportValidator
    {
        private readonly ILegalTypeValidator legalTypeValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly IKontragentsValidator kontragentsValidator;

        public WithdrawalOfProfitImportValidator(
            ILegalTypeValidator legalTypeValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            IKontragentsValidator kontragentsValidator)
        {
            this.legalTypeValidator = legalTypeValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.kontragentsValidator = kontragentsValidator;
        }

        public async Task ValidateAsync(WithdrawalOfProfitImportRequest request)
        {
            await legalTypeValidator.ValidateForIpAsync();
            await settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
            if (request.OperationState != OperationState.MissingKontragent)
            {
                await kontragentsValidator.ValidateAsync(request.Kontragent.Id);
            }
        }
    }
}