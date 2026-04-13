using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class MediationFeeMapper
    {
        internal static MediationFeeStateDefinition.State MapToState(
            this MediationFeeCreated eventData,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            SettlementAccountDto settlementAccount,
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
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),
                LinkedBills = eventData.BillLinks
                    .Select(bill => bill.MapToDefinitionState(linkedDocuments.GetValueOrDefault(bill.BillBaseId)))
                    .ToArray()
            };
        }

        internal static MediationFeeStateDefinition.State MapToState(
            this MediationFeeUpdated eventData,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            SettlementAccountDto settlementAccount,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),
                LinkedBills = eventData.BillLinks
                    .Select(bill => bill.MapToDefinitionState(linkedDocuments.GetValueOrDefault(bill.BillBaseId)))
                    .ToArray()
            };
        }

        internal static MediationFeeStateDefinition.State MapToState(
            this MediationFeeProvideRequired eventData,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            SettlementAccountDto settlementAccount,
            ContractDto contract)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,

                LinkedDocuments = (eventData.DocumentLinks ?? Array.Empty<DocumentLink>())
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),
                LinkedBills = (eventData.BillLinks ?? Array.Empty<BillLink>())
                    .Select(bill => bill.MapToDefinitionState(linkedDocuments.GetValueOrDefault(bill.BillBaseId)))
                    .ToArray()
            };
        }

        internal static MediationFeeStateDefinition.State MapToState(
            this MediationFeeDeleted eventData)
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
