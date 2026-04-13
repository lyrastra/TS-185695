using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPurchase.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class IncomingCurrencyPurchaseMapper
    {
        internal static IncomingCurrencyPurchaseStateDefinition.State MapToState(
            this IncomingCurrencyPurchaseCreatedMessage eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto fromSettlementAccount)
        {
            var sumCurrency = settlementAccount.Currency.ToString();
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                FromSettlementAccountId = eventData.FromSettlementAccountId,
                FromSettlementAccountNumber = fromSettlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, sumCurrency),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static IncomingCurrencyPurchaseStateDefinition.State MapToState(
            this IncomingCurrencyPurchaseUpdatedMessage eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto fromSettlementAccount)
        {
            var sumCurrency = settlementAccount.Currency.ToString();
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                FromSettlementAccountId = eventData.FromSettlementAccountId,
                FromSettlementAccountNumber = fromSettlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, sumCurrency),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static IncomingCurrencyPurchaseStateDefinition.State MapToState(
            this IncomingCurrencyPurchaseDeletedMessage eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
