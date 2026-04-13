using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class TransferFromAccountMapper
    {
        internal static TransferFromAccountStateDefinition.State MapToState(
            this TransferFromAccountCreatedMessage eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto fromSettlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                FromSettlementAccountId = eventData.FromSettlementAccountId,
                FromSettlementAccountNumber = fromSettlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description
            };
        }

        internal static TransferFromAccountStateDefinition.State MapToState(
            this TransferFromAccountUpdatedMessage eventData,
            SettlementAccountDto settlementAccount,
            SettlementAccountDto fromSettlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                FromSettlementAccountId = eventData.FromSettlementAccountId,
                FromSettlementAccountNumber = fromSettlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description
            };
        }

        internal static TransferFromAccountStateDefinition.State MapToState(
            this TransferFromAccountDeletedMessage eventData)
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
