using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.TransferToSettlementAccount.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PurseOperations.Outgoing
{
    internal static class TransferToSettlementAccountMapper
    {
        internal static TransferToSettlementAccountStateDefinition.State MapToState(
            this TransferToSettlementAccountCreated eventData,
            PurseDto purse,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                PurseId = eventData.PurseId,
                PurseName = purse?.Name,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Comment = eventData.Comment,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferToSettlementAccountStateDefinition.State MapToState(
            this TransferToSettlementAccountUpdated eventData,
            PurseDto purse,
            SettlementAccountDto settlementAccount)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                PurseId = eventData.PurseId,
                PurseName = purse?.Name,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.PurseOperationTransferToSettlement
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferToSettlementAccountStateDefinition.State MapToState(
            this TransferToSettlementAccountDeleted eventData)
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
