using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class CurrencyPaymentToSupplierMapper
    {
        internal static CurrencyPaymentToSupplierStateDefinition.State MapToState(
            this CurrencyPaymentToSupplierCreatedMessage eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            ContractDto contract)
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
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId), sumCurrency))
                    .ToArray()
            };
        }

        internal static CurrencyPaymentToSupplierStateDefinition.State MapToState(
            this CurrencyPaymentToSupplierUpdatedMessage eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            ContractDto contract)
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
                Contractor = eventData.Contractor.MapToDefinitionState(),
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                LinkedDocuments = eventData.DocumentLinks
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId), sumCurrency))
                    .ToArray()
            };
        }

        internal static CurrencyPaymentToSupplierStateDefinition.State MapToState(
            this CurrencyPaymentToSupplierDeletedMessage eventData)
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
