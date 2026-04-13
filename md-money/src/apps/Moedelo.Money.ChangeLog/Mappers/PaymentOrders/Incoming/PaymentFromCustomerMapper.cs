using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class PaymentFromCustomerMapper
    {
        internal static PaymentFromCustomerStateDefinition.State MapToState(
            this PaymentFromCustomerCreated eventData,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            SettlementAccountDto settlementAccount,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                IsMediation = eventData.IsMediation,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                IsMainContractor = eventData.IsMediation == false
                    ? eventData.IsMainContractor
                    : null,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,
                MediationCommissionSum = eventData.MediationCommissionSum.HasValue ? MoneySum.InRubles(eventData.MediationCommissionSum.Value) : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                PatentId = eventData.PatentId,

                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),
                LinkedBills = eventData.BillLinks
                    .Select(bill => bill.MapToDefinitionState(linkedDocuments.GetValueOrDefault(bill.BillBaseId)))
                    .ToArray(),
                ReserveSum = eventData.ReserveSum.HasValue ? MoneySum.InRubles(eventData.ReserveSum.Value) : null,
            };
        }

        internal static PaymentFromCustomerStateDefinition.State MapToState(
            this PaymentFromCustomerUpdated eventData,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            SettlementAccountDto settlementAccount,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                IsMediation = eventData.IsMediation,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                IsMainContractor = eventData.IsMediation == false
                    ? eventData.IsMainContractor
                    : null,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,
                MediationCommissionSum = eventData.MediationCommissionSum.HasValue ? MoneySum.InRubles(eventData.MediationCommissionSum.Value) : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                PatentId = eventData.PatentId,

                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),
                LinkedBills = eventData.BillLinks
                    .Select(bill => bill.MapToDefinitionState(linkedDocuments.GetValueOrDefault(bill.BillBaseId)))
                    .ToArray(),
                ReserveSum = eventData.ReserveSum.HasValue ? MoneySum.InRubles(eventData.ReserveSum.Value) : null,
            };
        }

        internal static PaymentFromCustomerStateDefinition.State MapToState(
            this PaymentFromCustomerProvideRequired eventData,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            SettlementAccountDto settlementAccount,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                IsMediation = eventData.IsMediation,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                IsMainContractor = eventData.IsMediation == false
                    ? eventData.IsMainContractor
                    : null,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,
                MediationCommissionSum = eventData.MediationCommissionSum.HasValue ? MoneySum.InRubles(eventData.MediationCommissionSum.Value) : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                PatentId = eventData.PatentId,

                LinkedDocuments = (eventData.DocumentLinks ?? Array.Empty<DocumentLink>())
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),
                LinkedBills = (eventData.BillLinks ?? Array.Empty<BillLink>())
                    .Select(bill => bill.MapToDefinitionState(linkedDocuments.GetValueOrDefault(bill.BillBaseId)))
                    .ToArray(),
                ReserveSum = eventData.ReserveSum.HasValue ? MoneySum.InRubles(eventData.ReserveSum.Value) : null,
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

        internal static (string, object) MapToFieldState(
            this PaymentFromCustomerSetReserve eventData)
        {
            return (
                nameof(PaymentFromCustomerStateDefinition.State.ReserveSum),
                eventData.ReserveSum.HasValue ? MoneySum.InRubles(eventData.ReserveSum.Value) : null
            );
        }
    }
}
