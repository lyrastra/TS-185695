using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class OutgoingCurrencyPurchaseMapper
    {
        internal static OutgoingCurrencyPurchaseStateDefinition.State MapToState(
            this OutgoingCurrencyPurchaseCreated eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto toSettlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                ToSettlementAccountId = eventData.ToSettlementAccountId ?? 0,
                ToSettlementAccountNumber = toSettlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                ExchangeRate = MoneySum.InRubles(eventData.ExchangeRate),
                ExchangeRateDiff = MoneySum.InRubles(eventData.ExchangeRateDiff),
                TotalSum = new MoneySum(eventData.TotalSum, toSettlementAccount?.Currency.ToString()),
                Description = eventData.Description,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static OutgoingCurrencyPurchaseStateDefinition.State MapToState(
            this OutgoingCurrencyPurchaseUpdated eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto toSettlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                ToSettlementAccountId = eventData.ToSettlementAccountId,
                ToSettlementAccountNumber = toSettlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                ExchangeRate = MoneySum.InRubles(eventData.ExchangeRate),
                ExchangeRateDiff = MoneySum.InRubles(eventData.ExchangeRateDiff),
                TotalSum = new MoneySum(eventData.TotalSum, toSettlementAccount?.Currency.ToString()),
                Description = eventData.Description,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static OutgoingCurrencyPurchaseStateDefinition.State MapToState(
            this OutgoingCurrencyPurchaseDeleted eventData)
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
