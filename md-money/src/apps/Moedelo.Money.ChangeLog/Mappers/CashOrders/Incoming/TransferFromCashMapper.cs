using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.TransferFromCash.Events;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Incoming
{
    internal static class TransferFromCashMapper
    {
        internal static TransferFromCashStateDefinition.State MapToState(
            this TransferFromCashCreated eventData,
            CashDto cash,
            CashDto fromCash)
        {
            return new TransferFromCashStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                FromCashId = eventData.FromCashId,
                FromCashName = fromCash.MapToName(eventData.FromCashId),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                Sum = MoneySum.InRubles(eventData.Sum),
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferFromCashStateDefinition.State MapToState(
            this TransferFromCashUpdated eventData,
            CashDto cash,
            CashDto fromCash)
        {
            return new TransferFromCashStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                FromCashId = eventData.FromCashId,
                FromCashName = fromCash.MapToName(eventData.FromCashId),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                Sum = MoneySum.InRubles(eventData.Sum),
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                ProvideInAccounting = eventData.ProvideInAccounting,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderIncomingFromAnotherCash
                    ? eventData.OldOperationType.GetDescription()
                    : null
            };
        }

        internal static TransferFromCashStateDefinition.State MapToState(
            this TransferFromCashDeleted eventData)
        {
            return new TransferFromCashStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
