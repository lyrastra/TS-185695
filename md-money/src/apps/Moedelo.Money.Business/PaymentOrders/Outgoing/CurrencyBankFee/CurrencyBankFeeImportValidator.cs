using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyBankFee
{
    [InjectAsSingleton(typeof(CurrencyBankFeeImportValidator))]
    class CurrencyBankFeeImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;

        public CurrencyBankFeeImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessValidator = accessValidator;
        }

        public async Task ValidateAsync(CurrencyBankFeeImportRequest request)
        {
            await accessValidator.ValidateAsync();
            await settlementAccountsValidator.ValidateCurrencyAsync(request.SettlementAccountId).ConfigureAwait(false);
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year).ConfigureAwait(false);
        }
    }
}