using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class TransferFromCashCollectionMapper
    {
        internal static TransferFromCashCollectionStateDefinition.State MapToState(
            this TransferFromCashCollectionCreatedMessage eventData,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferFromCashCollectionStateDefinition.State MapToState(
            this TransferFromCashCollectionUpdatedMessage eventData,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferFromCashCollectionStateDefinition.State MapToState(
            this TransferFromCashCollectionDeletedMessage eventData)
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
