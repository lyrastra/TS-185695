using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Incoming;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Incoming.PaymentFromCustomer.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.PurseOperations.Incoming
{
    internal static class PaymentFromCustomerMapper
    {
        internal static PaymentFromCustomerStateDefinition.State MapToState(
            this PaymentFromCustomerCreated eventData,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            PurseDto purse,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent,
            bool isOsno)
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
                WithNds = isOsno
                    ? eventData.Nds.IsWithNds()
                    : null,
                NdsType = isOsno
                    ? eventData.Nds.GetNdsType()
                    : null,
                NdsSum = isOsno
                    ? eventData.Nds.GetNdsSum()
                    : null,
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType?.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),

                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),

                BillBaseId = eventData.BillBaseId,
                BillName = eventData.BillBaseId.HasValue
                    ? linkedDocuments.GetValueOrDefault(eventData.BillBaseId.Value).MapToName()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static PaymentFromCustomerStateDefinition.State MapToState(
            this PaymentFromCustomerUpdated eventData,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            PurseDto purse,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent,
            bool isOsno)
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
                WithNds = isOsno
                    ? eventData.Nds.IsWithNds()
                    : null,
                NdsType = isOsno
                    ? eventData.Nds.GetNdsType()
                    : null,
                NdsSum = isOsno
                    ? eventData.Nds.GetNdsSum()
                    : null,
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType?.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),

                BillBaseId = eventData.BillBaseId,
                BillName = eventData.BillBaseId.HasValue
                    ? linkedDocuments.GetValueOrDefault(eventData.BillBaseId.Value).MapToName()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static PaymentFromCustomerStateDefinition.State MapToState(
            this PaymentFromCustomerProvided eventData,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            PurseDto purse,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent,
            bool isOsno)
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
                WithNds = isOsno
                    ? eventData.Nds.IsWithNds()
                    : null,
                NdsType = isOsno
                    ? eventData.Nds.GetNdsType()
                    : null,
                NdsSum = isOsno
                    ? eventData.Nds.GetNdsSum()
                    : null,
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType?.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),

                LinkedDocuments = (eventData.DocumentLinks ?? Array.Empty<DocumentLink>())
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),

                BillBaseId = eventData.BillBaseId,
                BillName = eventData.BillBaseId.HasValue
                    ? linkedDocuments.GetValueOrDefault(eventData.BillBaseId.Value).MapToName()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static PaymentFromCustomerStateDefinition.State MapToState(
            this PaymentFromCustomerDeleted eventData)
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
