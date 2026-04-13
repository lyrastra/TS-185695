using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Validation;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;
using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.Exceptions;
using Moedelo.Money.Business.ExchangeRates;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IncomingCurrencyPurchaseImportValidator))]
    internal sealed class IncomingCurrencyPurchaseImportValidator
    {
        private readonly ISettlementAccountsValidator settlementAccountsValidator;
        private readonly ITaxationSystemValidator taxationSystemValidator;
        private readonly ICurrencyOperationsAccessValidator accessAccessValidator;
        private readonly IExchangeRatesReader exchangeRatesReader;

        public IncomingCurrencyPurchaseImportValidator(
            ISettlementAccountsValidator settlementAccountsValidator,
            ITaxationSystemValidator taxationSystemValidator, 
            ICurrencyOperationsAccessValidator accessAccessValidator,
            IExchangeRatesReader exchangeRatesReader)
        {
            this.settlementAccountsValidator = settlementAccountsValidator;
            this.taxationSystemValidator = taxationSystemValidator;
            this.accessAccessValidator = accessAccessValidator;
            this.exchangeRatesReader = exchangeRatesReader;
        }
        
        public async Task ValidateAsync(IncomingCurrencyPurchaseImportRequest request)
        {
            await accessAccessValidator.ValidateAsync();
            await taxationSystemValidator.ValidateUsnAsync(request.Date.Year);
            var (_, currency) = await settlementAccountsValidator.ValidateRubAndCurrencyAsync(request.FromSettlementAccountId, request.SettlementAccountId);
            if (!await exchangeRatesReader.ExistsRateAsync(request.Date, currency.Currency))
            {
                throw new BusinessValidationException(nameof(request.SettlementAccountId), $"Для счёта с идентификатором {request.SettlementAccountId} не найден курс валюты ({currency.Currency}) на дату операции");
            }
        }
    }
}