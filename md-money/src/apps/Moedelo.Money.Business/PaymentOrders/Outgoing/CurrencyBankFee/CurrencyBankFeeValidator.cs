using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyBankFee
{
    [InjectAsSingleton(typeof(ICurrencyBankFeeValidator))]
    class CurrencyBankFeeValidator : ICurrencyBankFeeValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;

        public CurrencyBankFeeValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessValidator, TaxPostingsValidator taxPostingsValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessValidator = accessValidator;
            this.taxPostingsValidator = taxPostingsValidator;
        }

        public async Task ValidateAsync(CurrencyBankFeeSaveRequest request)
        {
            await accessValidator.ValidateAsync();
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateCurrencyAsync(request.SettlementAccountId).ConfigureAwait(false);
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year).ConfigureAwait(false);
            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
        }
    }
}