using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromSettlementAccount.Events;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Incoming
{
    internal static class TransferFromSettlementAccountMapper
    {
        internal static TransferFromSettlementAccountStateDefinition.State MapToState(
            this TransferFromSettlementAccountCreated eventData,
            CashDto cash)
        {
            return new TransferFromSettlementAccountStateDefinition.State
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

        internal static TransferFromSettlementAccountStateDefinition.State MapToState(
            this TransferFromSettlementAccountUpdated eventData,
            CashDto cash)
        {
            return new TransferFromSettlementAccountStateDefinition.State
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
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderIncomingFromSettlementAccount
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferFromSettlementAccountStateDefinition.State MapToState(
            this TransferFromSettlementAccountDeleted eventData)
        {
            return new TransferFromSettlementAccountStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
