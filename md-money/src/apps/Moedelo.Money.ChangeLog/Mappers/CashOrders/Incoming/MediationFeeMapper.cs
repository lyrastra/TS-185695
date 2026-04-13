using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MediationFee.Events;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Incoming
{
    internal static class MediationFeeMapper
    {
        internal static MediationFeeStateDefinition.State MapToState(
            this MediationFeeCreated eventData,
            CashDto cash,
            ContractDto contract,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments)
        {
            return new MediationFeeStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                Destination = eventData.Destination,
                Comment = eventData.Comment,

                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvideInAccounting = eventData.ProvideInAccounting,

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
            CashDto cash,
            ContractDto contract,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments)
        {
            return new MediationFeeStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderIncomingMediationFee
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvideInAccounting = eventData.ProvideInAccounting,

                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),
                LinkedBills = eventData.BillLinks
                    .Select(bill => bill.MapToDefinitionState(linkedDocuments.GetValueOrDefault(bill.BillBaseId)))
                    .ToArray()
            };
        }

        internal static MediationFeeStateDefinition.State MapToState(
            this MediationFeeProvided eventData,
            CashDto cash,
            ContractDto contract,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments)
        {
            return new MediationFeeStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                Destination = eventData.Destination,
                Comment = eventData.Comment,

                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvideInAccounting = eventData.ProvideInAccounting,

                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId)))
                    .ToArray(),
                LinkedBills = eventData.BillLinks
                    .Select(bill => bill.MapToDefinitionState(linkedDocuments.GetValueOrDefault(bill.BillBaseId)))
                    .ToArray()
            };
        }

        internal static MediationFeeStateDefinition.State MapToState(
            this MediationFeeDeleted eventData)
        {
            return new MediationFeeStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
