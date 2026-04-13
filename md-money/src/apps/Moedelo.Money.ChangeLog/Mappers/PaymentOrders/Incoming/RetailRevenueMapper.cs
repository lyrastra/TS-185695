using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class RetailRevenueMapper
    {
        internal static RetailRevenueStateDefinition.State MapToState(
            this RetailRevenueCreated eventData,
            SettlementAccountDto settlementAccount,
            PatentWithoutAdditionalDataDto patent,
            bool isUsn)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                IsMediation = isUsn
                    ? eventData.IsMediation
                    : null,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                AcquiringCommissionSum = eventData.AcquiringCommissionSum.HasValue
                    ? MoneySum.InRubles(eventData.AcquiringCommissionSum.Value)
                    : null,
                AcquiringCommissionDate = eventData.AcquiringCommissionDate,
                SaleDate = isUsn
                    ? eventData.SaleDate
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId)
            };
        }

        internal static RetailRevenueStateDefinition.State MapToState(
            this RetailRevenueUpdated eventData,
            SettlementAccountDto settlementAccount,
            PatentWithoutAdditionalDataDto patent,
            bool isUsn)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountNumber = settlementAccount?.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                IsMediation = isUsn
                    ? eventData.IsMediation
                    : null,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                AcquiringCommissionSum = eventData.AcquiringCommissionSum.HasValue
                    ? MoneySum.InRubles(eventData.AcquiringCommissionSum.Value)
                    : null,
                AcquiringCommissionDate = eventData.AcquiringCommissionDate,
                SaleDate = isUsn
                    ? eventData.SaleDate
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId)
            };
        }

        internal static RetailRevenueStateDefinition.State MapToState(
            this RetailRevenueDeleted eventData)
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
