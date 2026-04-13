using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.CashCollection.Events;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing
{
    internal static class CashCollectionMapper
    {
        internal static CashCollectionStateDefinition.State MapToState(
            this CashCollectionCreated eventData,
            CashDto cash)
        {
            return new CashCollectionStateDefinition.State
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

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static CashCollectionStateDefinition.State MapToState(
            this CashCollectionUpdated eventData,
            CashDto cash)
        {
            return new CashCollectionStateDefinition.State
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
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderOutgoingCollectionOfMoney
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static CashCollectionStateDefinition.State MapToState(
            this CashCollectionDeleted eventData)
        {
            return new CashCollectionStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
