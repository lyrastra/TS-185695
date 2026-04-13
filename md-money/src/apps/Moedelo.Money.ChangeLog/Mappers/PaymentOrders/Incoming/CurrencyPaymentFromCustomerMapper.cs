using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class CurrencyPaymentFromCustomerMapper
    {
        internal static CurrencyPaymentFromCustomerStateDefinition.State MapToState(
            this CurrencyPaymentFromCustomerCreated eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent)
        {
            var sumCurrency = settlementAccount.Currency.ToString();
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, sumCurrency),
                TotalSum = MoneySum.InRubles(eventData.TotalSum),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                PatentId = eventData.PatentId,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                LinkedDocuments = eventData.LinkedDocuments
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId), sumCurrency))
                    .ToArray()
            };
        }

        internal static CurrencyPaymentFromCustomerStateDefinition.State MapToState(
            this CurrencyPaymentFromCustomerUpdated eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent)
        {
            var sumCurrency = settlementAccount.Currency.ToString();
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, sumCurrency),
                TotalSum = MoneySum.InRubles(eventData.TotalSum),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                TaxationSystemType = eventData.TaxationSystemType?.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                PatentId = eventData.PatentId,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                LinkedDocuments = eventData.LinkedDocuments
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId), sumCurrency))
                    .ToArray()
            };
        }

        internal static CurrencyPaymentFromCustomerStateDefinition.State MapToState(
            this CurrencyPaymentFromCustomerProvideRequired eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent)
        {
            var sumCurrency = settlementAccount.Currency.ToString();
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = new MoneySum(eventData.Sum, sumCurrency),
                TotalSum = MoneySum.InRubles(eventData.TotalSum),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                PatentId = eventData.PatentId,
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                LinkedDocuments = eventData.LinkedDocuments
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId), sumCurrency))
                    .ToArray()
            };
        }

        internal static CurrencyPaymentFromCustomerStateDefinition.State MapToState(
            this CurrencyPaymentFromCustomerDeleted eventData)
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
