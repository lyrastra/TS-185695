using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToSupplier.Events;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing
{
    internal static class PaymentToSupplierMapper
    {
        internal static PaymentToSupplierStateDefinition.State MapToState(
            this PaymentToSupplierCreated eventData,
            CashDto cash,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                IsMainContractor = eventData.IsMainContractor,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Destination = eventData.Destination,
                Comment = eventData.Comment,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray()
            };
        }

        internal static PaymentToSupplierStateDefinition.State MapToState(
            this PaymentToSupplierUpdated eventData,
            CashDto cash,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                IsMainContractor = eventData.IsMainContractor,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderOutgoingPaymentSuppliersForGoods
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray()
            };
        }

        internal static PaymentToSupplierStateDefinition.State MapToState(
            this PaymentToSupplierProvided eventData,
            CashDto cash,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                IsMainContractor = eventData.IsMainContractor,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Destination = eventData.Destination,
                Comment = eventData.Comment,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                LinkedDocuments = (eventData.DocumentLinks ?? Array.Empty<DocumentLink>())
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray()
            };
        }

        internal static PaymentToSupplierStateDefinition.State MapToState(
            this PaymentToSupplierDeleted eventData)
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
