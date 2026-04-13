using System;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Business.Validation.TaxPostings;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale
{
    [InjectAsSingleton(typeof(IOutgoingCurrencySaleValidator))]
    internal sealed class OutgoingCurrencySaleValidator : IOutgoingCurrencySaleValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;
        private readonly TaxPostingsValidator taxPostingsValidator;

        public OutgoingCurrencySaleValidator(
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

        public async Task ValidateAsync(OutgoingCurrencySaleSaveRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year);
            await closedPeriodValidator.ValidateAsync(request.Date);
            await settlementAccountsValidator.ValidateRubAndCurrencyAsync(request.ToSettlementAccountId, request.SettlementAccountId, false);
            await taxPostingsValidator.ValidateAsync(request.Date, request.TaxPostings);
            ValidateOperationDate(request.Date);
            ValidateTaxPostingsSum(request);
        }

        private void ValidateOperationDate(DateTime operationDate) {
            if (operationDate > DateTime.Now) {
                throw new BusinessValidationException("Date", $"Будущая дата запрещена");
            }
        }

        private void ValidateTaxPostingsSum(OutgoingCurrencySaleSaveRequest request)
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