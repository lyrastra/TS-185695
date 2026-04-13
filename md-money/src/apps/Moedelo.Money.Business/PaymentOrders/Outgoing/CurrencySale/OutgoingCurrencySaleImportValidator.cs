using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale
{
    [InjectAsSingleton(typeof(OutgoingCurrencySaleImportValidator))]
    internal sealed class OutgoingCurrencySaleImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;

        public OutgoingCurrencySaleImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator, 
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessValidator = accessValidator;
        }

        public async Task ValidateAsync(OutgoingCurrencySaleImportRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year).ConfigureAwait(false);
            await settlementAccountsValidator.ValidateRubAndCurrencyAsync(request.ToSettlementAccountId, request.SettlementAccountId).ConfigureAwait(false);
        }
    }
}