using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.Other.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PurseOperations.Outgoing
{
    internal static class OtherOutgoingMapper
    {
        internal static OtherOutgoingStateDefinition.State MapToState(
            this OtherOutgoingCreated eventData,
            BaseDocumentDto billLink,
            PurseDto purse,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                PurseId = eventData.PurseId,
                PurseName = purse?.Name,
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                Comment = eventData.Comment,

                BillBaseId = eventData.BillBaseId,
                BillName = billLink?.MapToName(),

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static OtherOutgoingStateDefinition.State MapToState(
            this OtherOutgoingUpdated eventData,
            BaseDocumentDto billLink,
            PurseDto purse,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                PurseId = eventData.PurseId,
                PurseName = purse?.Name,
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.PurseOperationOtherOutgoing
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                BillBaseId = eventData.BillBaseId,
                BillName = billLink?.MapToName(),

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static OtherOutgoingStateDefinition.State MapToState(
            this OtherOutgoingDeleted eventData)
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
