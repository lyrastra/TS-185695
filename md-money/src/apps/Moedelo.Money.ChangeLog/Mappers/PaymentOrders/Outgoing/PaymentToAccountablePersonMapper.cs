using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class PaymentToAccountablePersonMapper
    {
        internal static PaymentToAccountablePersonStateDefinition.State MapToState(
            this PaymentToAccountablePersonCreated eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, LinkWithDocumentDto> advanceStatements)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                AdvanceStatementLinks = eventData.AdvanceStatementBaseIds
                    .Select(documentBaseId => advanceStatements.GetValueOrDefault(documentBaseId).MapToDefinitionState(documentBaseId))
                    .ToArray()
            };
        }

        internal static PaymentToAccountablePersonStateDefinition.State MapToState(
            this PaymentToAccountablePersonUpdated eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, LinkWithDocumentDto> advanceStatements)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                AdvanceStatementLinks = eventData.AdvanceStatementBaseIds
                    .Select(documentBaseId => advanceStatements.GetValueOrDefault(documentBaseId).MapToDefinitionState(documentBaseId))
                    .ToArray()
            };
        }

        internal static PaymentToAccountablePersonStateDefinition.State MapToState(
            this PaymentToAccountablePersonProvideRequired eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, LinkWithDocumentDto> advanceStatements)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                AdvanceStatementLinks = eventData.AdvanceStatementBaseIds
                    .Select(documentBaseId => advanceStatements.GetValueOrDefault(documentBaseId).MapToDefinitionState(documentBaseId))
                    .ToArray()
            };
        }

        internal static PaymentToAccountablePersonStateDefinition.State MapToState(
            this PaymentToAccountablePersonDeleted eventData)
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
