using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.RefundToCustomer.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing
{
    internal static class RefundToCustomerMapper
    {
        internal static RefundToCustomerStateDefinition.State MapToState(
            this RefundToCustomerCreatedMessage eventData,
            CashDto cash,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent,
            bool isOsno)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                IsMainContractor = eventData.IsMainContractor,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = isOsno
                    ? eventData.Nds.IsWithNds()
                    : null,
                NdsType = isOsno
                    ? eventData.Nds.GetNdsType()
                    : null,
                NdsSum = isOsno
                    ? eventData.Nds.GetNdsSum()
                    : null,
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentId = eventData.PatentId,
                PatentName = patent.MapToName(eventData.PatentId),

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static RefundToCustomerStateDefinition.State MapToState(
            this RefundToCustomerUpdatedMessage eventData,
            CashDto cash,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent,
            bool isOsno)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                IsMainContractor = eventData.IsMainContractor,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                ContractBaseId = eventData.ContractBaseId,
                Sum = MoneySum.InRubles(eventData.Sum),
                WithNds = isOsno
                    ? eventData.Nds.IsWithNds()
                    : null,
                NdsType = isOsno
                    ? eventData.Nds.GetNdsType()
                    : null,
                NdsSum = isOsno
                    ? eventData.Nds.GetNdsSum()
                    : null,
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentId = eventData.PatentId,
                PatentName = patent.MapToName(eventData.PatentId),
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderOutgoingReturnToBuyer
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static RefundToCustomerStateDefinition.State MapToState(
            this RefundToCustomerDeletedMessage eventData)
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
