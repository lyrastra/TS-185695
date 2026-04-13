using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale
{
    [InjectAsSingleton(typeof(IncomingCurrencySaleImportValidator))]
    internal sealed class IncomingCurrencySaleImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;

        public IncomingCurrencySaleImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator, 
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessValidator = accessValidator;
        }
        
        public async Task ValidateAsync(IncomingCurrencySaleImportRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year).ConfigureAwait(false);
            await ValidateSettlementAccountsAsync(request).ConfigureAwait(false);
        }

        private Task ValidateSettlementAccountsAsync(IncomingCurrencySaleImportRequest request)
        {
            if (request.FromSettlementAccountId == null &&
                request.OperationState != OperationState.MissingCurrencySettlementAccount)
            {
                throw new BusinessValidationException("FromSettlementAccountId", "Это поле должно быть заполнено.");
            }
            return request.FromSettlementAccountId.HasValue
                ? settlementAccountsValidator.ValidateRubAndCurrencyAsync(
                    request.SettlementAccountId, 
                    request.FromSettlementAccountId.Value, 
                    false)
                : settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
        }
    }
}