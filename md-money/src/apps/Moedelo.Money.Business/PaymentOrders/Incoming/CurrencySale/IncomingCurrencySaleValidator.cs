using System;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale
{
    [InjectAsSingleton(typeof(IIncomingCurrencySaleValidator))]
    internal sealed class IncomingCurrencySaleValidator : IIncomingCurrencySaleValidator
    {
        private readonly IClosedPeriodValidator closedPeriodValidator;
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessValidator;

        public IncomingCurrencySaleValidator(
            IClosedPeriodValidator closedPeriodValidator, 
            ISettlementAccountsValidator settlementAccountsValidator, 
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessValidator)
        {
            this.closedPeriodValidator = closedPeriodValidator;
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessValidator = accessValidator;
        }
        
        public async Task ValidateAsync(IncomingCurrencySaleSaveRequest request)
        {
            await accessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year);
            await closedPeriodValidator.ValidateAsync(request.Date);
            await ValidateSettlementAccountsAsync(request);
            ValidateOperationDate(request.Date);
        }

        private void ValidateOperationDate(DateTime operationDate) {
            if(operationDate > DateTime.Now) {
                throw new BusinessValidationException("Date", $"Будущая дата запрещена");
            }
        }

        private Task ValidateSettlementAccountsAsync(IncomingCurrencySaleSaveRequest request)
        {
            if (request.FromSettlementAccountId == null &&
                request.OperationState != OperationState.MissingCurrencySettlementAccount)
            {
                throw new BusinessValidationException("FromSettlementAccountId", "Это поле должно быть заполнено.");
            }
            return request.FromSettlementAccountId.HasValue
                ? settlementAccountsValidator.ValidateRubAndCurrencyAsync(request.SettlementAccountId, request.FromSettlementAccountId.Value, false)
                : settlementAccountsValidator.ValidateAsync(request.SettlementAccountId);
        }
    }
}