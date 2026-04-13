using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.TransferToSettlementAccount.Events;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing
{
    internal static class TransferToSettlementAccountMapper
    {
        internal static TransferToSettlementAccountStateDefinition.State MapToState(
            this TransferToSettlementAccountCreated eventData,
            CashDto cash)
        {
            return new TransferToSettlementAccountStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                Sum = MoneySum.InRubles(eventData.Sum),
                Destination = eventData.Destination,
                Comment = eventData.Comment,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferToSettlementAccountStateDefinition.State MapToState(
            this TransferToSettlementAccountUpdated eventData,
            CashDto cash)
        {
            return new TransferToSettlementAccountStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderOutcomingToSettlementAccount
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferToSettlementAccountStateDefinition.State MapToState(
            this TransferToSettlementAccountDeleted eventData)
        {
            return new TransferToSettlementAccountStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
