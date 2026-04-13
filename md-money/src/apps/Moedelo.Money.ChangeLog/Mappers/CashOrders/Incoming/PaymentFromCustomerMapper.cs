using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.PaymentFromCustomer.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Incoming
{
    internal static class PaymentFromCustomerMapper
    {
        internal static PaymentFromCustomerStateDefinition.State MapToState(
            this PaymentFromCustomerCreated eventData,
            CashDto cash,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments)
        {
            return new PaymentFromCustomerStateDefinition.State
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
                IsMainContractor = eventData.IsMainContractor,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                IsMediation = eventData.IsMediation,
                MediationCommissionSum = eventData.MediationCommissionSum.HasValue
                    ? MoneySum.InRubles(eventData.MediationCommissionSum.Value)
                    : null,
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentId = eventData.PatentId,
                PatentName = patent.MapToName(eventData.PatentId),

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

        internal static PaymentFromCustomerStateDefinition.State MapToState(
            this PaymentFromCustomerUpdated eventData,
            CashDto cash,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments)
        {
            return new PaymentFromCustomerStateDefinition.State
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
                IsMainContractor = eventData.IsMainContractor,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                IsMediation = eventData.IsMediation,
                MediationCommissionSum = eventData.MediationCommissionSum.HasValue
                    ? MoneySum.InRubles(eventData.MediationCommissionSum.Value)
                    : null,
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentId = eventData.PatentId,
                PatentName = patent.MapToName(eventData.PatentId),
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderIncomingPaymentForGoods
                    ? eventData.OldOperationType.GetDescription()
                    : null,

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

        internal static PaymentFromCustomerStateDefinition.State MapToState(
            this PaymentFromCustomerProvided eventData,
            CashDto cash,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments)
        {
            return new PaymentFromCustomerStateDefinition.State
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
                IsMainContractor = eventData.IsMainContractor,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                IsMediation = eventData.IsMediation,
                MediationCommissionSum = eventData.MediationCommissionSum.HasValue
                    ? MoneySum.InRubles(eventData.MediationCommissionSum.Value)
                    : null,
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentId = eventData.PatentId,
                PatentName = patent.MapToName(eventData.PatentId),

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

        internal static PaymentFromCustomerStateDefinition.State MapToState(
            this PaymentFromCustomerDeleted eventData)
        {
            return new PaymentFromCustomerStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
