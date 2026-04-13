using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class PaymentToSupplierMapper
    {
        internal static PaymentToSupplierStateDefinition.State MapToState(
            this PaymentToSupplierCreated eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                IsMainContractor = eventData.IsMainContractor,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),
                ReserveSum = eventData.ReserveSum.HasValue ? MoneySum.InRubles(eventData.ReserveSum.Value) : null,
            };
        }

        internal static PaymentToSupplierStateDefinition.State MapToState(
            this PaymentToSupplierUpdated eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                IsMainContractor = eventData.IsMainContractor,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),
                ReserveSum = eventData.ReserveSum.HasValue ? MoneySum.InRubles(eventData.ReserveSum.Value) : null,
            };
        }

        internal static PaymentToSupplierStateDefinition.State MapToState(
            this PaymentToSupplierProvideRequired eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                IsMainContractor = eventData.IsMainContractor,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                LinkedDocuments = (eventData.DocumentLinks ?? Array.Empty<DocumentLink>())
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),
                ReserveSum = eventData.ReserveSum.HasValue ? MoneySum.InRubles(eventData.ReserveSum.Value) : null,
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

        internal static (string, object) MapToFieldState(
            this PaymentToSupplierSetReserve eventData)
        {
            return new(
                nameof(PaymentToSupplierStateDefinition.State.ReserveSum),
                eventData.ReserveSum.HasValue ? MoneySum.InRubles(eventData.ReserveSum.Value).ValueAndCurrency : null
            );
        }
    }
}
