using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IIncomingCurrencyPurchaseValidator))]
    internal sealed class IncomingCurrencyPurchaseValidator : IIncomingCurrencyPurchaseValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessAccessValidator;

        public IncomingCurrencyPurchaseValidator(
            IClosedPeriodValidator closedPeriodValidator, 
            ISettlementAccountsValidator settlementAccountsValidator, 
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessAccessValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessAccessValidator = accessAccessValidator;
        }
        
        public async Task ValidateAsync(IncomingCurrencyPurchaseSaveRequest request)
        {
            await accessAccessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year).ConfigureAwait(false);
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateRubAndCurrencyAsync(request.FromSettlementAccountId.GetValueOrDefault(), request.SettlementAccountId, false).ConfigureAwait(false);
        }
    }
}