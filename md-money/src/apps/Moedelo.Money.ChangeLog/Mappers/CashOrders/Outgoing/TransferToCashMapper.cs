using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.TransferToCash.Events;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing
{
    internal static class TransferToCashMapper
    {
        internal static TransferToCashStateDefinition.State MapToState(
            this TransferToCashCreated eventData,
            CashDto cash,
            CashDto toCash)
        {
            return new TransferToCashStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                ToCashId = eventData.ToCashId,
                ToCashName = toCash.MapToName(eventData.ToCashId),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                Sum = MoneySum.InRubles(eventData.Sum),
                Destination = eventData.Destination,
                Comment = eventData.Comment,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferToCashStateDefinition.State MapToState(
            this TransferToCashUpdated eventData,
            CashDto cash,
            CashDto toCash)
        {
            return new TransferToCashStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                ToCashId = eventData.ToCashId,
                ToCashName = toCash.MapToName(eventData.ToCashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderOutgoingTranslatedToOtherCash
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static TransferToCashStateDefinition.State MapToState(
            this TransferToCashDeleted eventData)
        {
            return new TransferToCashStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
