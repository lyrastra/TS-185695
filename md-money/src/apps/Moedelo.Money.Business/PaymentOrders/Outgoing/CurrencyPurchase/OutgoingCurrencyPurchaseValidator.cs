using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Enums;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Money.Business.Validation.TaxPostings;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IOutgoingCurrencyPurchaseValidator))]
    internal sealed class OutgoingCurrencyPurchaseValidator : IOutgoingCurrencyPurchaseValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;

        public OutgoingCurrencyPurchaseValidator(
            IClosedPeriodValidator closedPeriodValidator,
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessValidator,
            TaxPostingsValidator taxPostingsValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessValidator = accessValidator;
            this.taxPostingsValidator = taxPostingsValidator;
        }

        public async Task ValidateAsync(OutgoingCurrencyPurchaseSaveRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year).ConfigureAwait(false);
            await closedPeriodValidator.ValidateAsync(request.Date).ConfigureAwait(false);
            await ValidateSettlementAccountsAsync(request).ConfigureAwait(false);
            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
            ValidateTaxPostingsSum(request);
        }

        private Task ValidateSettlementAccountsAsync(OutgoingCurrencyPurchaseSaveRequest request)
        {
            if (request.ToSettlementAccountId == null &&
                request.OperationState != OperationState.MissingCurrencySettlementAccount)
            {
                throw new BusinessValidationException("ToSettlementAccountId", "Это поле должно быть заполнено.");
            }
            return request.ToSettlementAccountId.HasValue
                ? settlementAccountsValidator.ValidateRubAndCurrencyAsync(request.SettlementAccountId, request.ToSettlementAccountId.Value, false)
                : settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
        }

        private void ValidateTaxPostingsSum(OutgoingCurrencyPurchaseSaveRequest request)
        {
            if (request.ExchangeRateDiff > 0 && request.TaxPostings?.UsnTaxPostings?.Count > 0)
            {
                var sum = request.TaxPostings.UsnTaxPostings.Sum(posting => posting.Sum);
                if (sum > request.ExchangeRateDiff)
                {
                    throw new BusinessValidationException("TaxPostings", $"Сумма проводок ({sum}) больше , чем курсовая разница ({request.ExchangeRateDiff}).");
                }
            }
        }
    }
}