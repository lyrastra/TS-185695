using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencySale.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class IncomingCurrencySaleMapper
    {
        internal static IncomingCurrencySaleStateDefinition.State MapToState(
            this IncomingCurrencySaleCreatedMessage eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto fromSettlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                FromSettlementAccountId = eventData.FromSettlementAccountId ?? 0,
                FromSettlementAccountNumber = fromSettlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static IncomingCurrencySaleStateDefinition.State MapToState(
            this IncomingCurrencySaleUpdatedMessage eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto fromSettlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                FromSettlementAccountId = eventData.FromSettlementAccountId,
                FromSettlementAccountNumber = fromSettlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static IncomingCurrencySaleStateDefinition.State MapToState(
            this IncomingCurrencySaleDeletedMessage eventData)
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
