using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Enums;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase
{
    [InjectAsSingleton(typeof(OutgoingCurrencyPurchaseImportValidator))]
    internal sealed class OutgoingCurrencyPurchaseImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;

        public OutgoingCurrencyPurchaseImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessValidator)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessValidator = accessValidator;
        }

        public async Task ValidateAsync(OutgoingCurrencyPurchaseImportRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year).ConfigureAwait(false);
            await ValidateSettlementAccountsAsync(request).ConfigureAwait(false);
        }

        private Task ValidateSettlementAccountsAsync(OutgoingCurrencyPurchaseImportRequest request)
        {
            if (request.ToSettlementAccountId == null &&
                request.OperationState != OperationState.MissingCurrencySettlementAccount)
            {
                throw new BusinessValidationException("ToSettlementAccountId", "Это поле должно быть заполнено.");
            }
            return request.ToSettlementAccountId.HasValue
                ? settlementAccountsValidator.ValidateRubAndCurrencyAsync(request.SettlementAccountId, request.ToSettlementAccountId.Value)
                : settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
        }
    }
}