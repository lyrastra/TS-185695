using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RetailRevenue.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Incoming
{
    internal static class RetailRevenueMapper
    {
        internal static RetailRevenueStateDefinition.State MapToState(
            this RetailRevenueCreated eventData,
            CashDto cash,
            PatentWithoutAdditionalDataDto patent)
        {
            return new RetailRevenueStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                ZReportNumber = eventData.ZReportNumber,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                PaidCardSum = eventData.PaidCardSum.HasValue
                    ? MoneySum.InRubles(eventData.PaidCardSum.Value)
                    : null,
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentId = eventData.PatentId,
                PatentName = patent.MapToName(eventData.PatentId),

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static RetailRevenueStateDefinition.State MapToState(
            this RetailRevenueUpdated eventData,
            CashDto cash,
            PatentWithoutAdditionalDataDto patent)
        {
            return new RetailRevenueStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                ZReportNumber = eventData.ZReportNumber,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                PaidCardSum = eventData.PaidCardSum.HasValue
                    ? MoneySum.InRubles(eventData.PaidCardSum.Value)
                    : null,
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                ContractorId = eventData.Contractor?.Id ?? 0,
                ContractorName = eventData.Contractor?.Name,
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentId = eventData.PatentId,
                PatentName = patent.MapToName(eventData.PatentId),
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderIncomingFromRetailRevenue
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static RetailRevenueStateDefinition.State MapToState(
            this RetailRevenueDeleted eventData)
        {
            return new RetailRevenueStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
