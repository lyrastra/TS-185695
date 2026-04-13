using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToAccountablePerson.Events;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing
{
    internal static class PaymentToAccountablePersonMapper
    {
        internal static PaymentToAccountablePersonStateDefinition.State MapToState(
            this PaymentToAccountablePersonCreated eventData,
            CashDto cash,
            IReadOnlyDictionary<long, LinkWithDocumentDto> advanceStatements)
        {
            return new PaymentToAccountablePersonStateDefinition.State
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

                ProvideInAccounting = eventData.ProvideInAccounting,

                AdvanceStatementLinks = eventData.AdvanceStatementBaseIds
                    .Select(documentBaseId => advanceStatements.GetValueOrDefault(documentBaseId).MapToDefinitionState(documentBaseId))
                    .ToArray()
            };
        }

        internal static PaymentToAccountablePersonStateDefinition.State MapToState(
            this PaymentToAccountablePersonUpdated eventData,
            CashDto cash,
            IReadOnlyDictionary<long, LinkWithDocumentDto> advanceStatements)
        {
            return new PaymentToAccountablePersonStateDefinition.State
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
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderOutgoingIssuanceAccountablePerson
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting,

                AdvanceStatementLinks = eventData.AdvanceStatementBaseIds
                    .Select(documentBaseId => advanceStatements.GetValueOrDefault(documentBaseId).MapToDefinitionState(documentBaseId))
                    .ToArray()
            };
        }

        internal static PaymentToAccountablePersonStateDefinition.State MapToState(
            this PaymentToAccountablePersonProvided eventData,
            CashDto cash,
            IReadOnlyDictionary<long, LinkWithDocumentDto> advanceStatements)
        {
            return new PaymentToAccountablePersonStateDefinition.State
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

                ProvideInAccounting = eventData.ProvideInAccounting,

                AdvanceStatementLinks = eventData.AdvanceStatementBaseIds
                    .Select(documentBaseId => advanceStatements.GetValueOrDefault(documentBaseId).MapToDefinitionState(documentBaseId))
                    .ToArray()
            };
        }

        internal static PaymentToAccountablePersonStateDefinition.State MapToState(
            this PaymentToAccountablePersonDeleted eventData)
        {
            return new PaymentToAccountablePersonStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
