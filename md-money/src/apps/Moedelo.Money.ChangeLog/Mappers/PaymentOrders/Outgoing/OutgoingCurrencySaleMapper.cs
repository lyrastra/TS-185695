using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class OutgoingCurrencySaleMapper
    {
        internal static OutgoingCurrencySaleStateDefinition.State MapToState(
            this OutgoingCurrencySaleCreated eventData,
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
                ToSettlementAccountId = eventData.ToSettlementAccountId,
                ToSettlementAccountNumber = toSettlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, settlementAccount?.Currency.ToString()),
                ExchangeRate = MoneySum.InRubles(eventData.ExchangeRate),
                ExchangeRateDiff = MoneySum.InRubles(eventData.ExchangeRateDiff),
                TotalSum = MoneySum.InRubles(eventData.TotalSum),
                Description = eventData.Description,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static OutgoingCurrencySaleStateDefinition.State MapToState(
            this OutgoingCurrencySaleUpdated eventData,
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
                Sum = new MoneySum(eventData.Sum, settlementAccount?.Currency.ToString()),
                ExchangeRate = MoneySum.InRubles(eventData.ExchangeRate),
                ExchangeRateDiff = MoneySum.InRubles(eventData.ExchangeRateDiff),
                TotalSum = MoneySum.InRubles(eventData.TotalSum),
                Description = eventData.Description,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static OutgoingCurrencySaleStateDefinition.State MapToState(
            this OutgoingCurrencySaleDeleted eventData)
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
