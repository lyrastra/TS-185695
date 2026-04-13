using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Incoming;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MiddlemanRetailRevenue.Events;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Incoming
{
    internal static class MiddlemanRetailRevenueMapper
    {
        internal static MiddlemanRetailRevenueStateDefinition.State MapToState(
            this MiddlemanRetailRevenueCreated eventData,
            CashDto cash,
            ContractDto contract)
        {
            return new MiddlemanRetailRevenueStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                PaidCardSum = eventData.PaidCardSum.HasValue
                    ? MoneySum.InRubles(eventData.PaidCardSum.Value)
                    : null,
                MyReward = eventData.MyReward.HasValue
                    ? MoneySum.InRubles(eventData.MyReward.Value)
                    : null,
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                Destination = eventData.Destination,
                Comment = eventData.Comment,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static MiddlemanRetailRevenueStateDefinition.State MapToState(
            this MiddlemanRetailRevenueUpdated eventData,
            CashDto cash,
            ContractDto contract)
        {
            return new MiddlemanRetailRevenueStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                PaidCardSum = eventData.PaidCardSum.HasValue
                    ? MoneySum.InRubles(eventData.PaidCardSum.Value)
                    : null,
                MyReward = eventData.MyReward.HasValue
                    ? MoneySum.InRubles(eventData.MyReward.Value)
                    : null,
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderIncomingMiddlemanRetailRevenue
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static MiddlemanRetailRevenueStateDefinition.State MapToState(
            this MiddlemanRetailRevenueDeleted eventData)
        {
            return new MiddlemanRetailRevenueStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
