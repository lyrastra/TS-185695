using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class RefundToCustomerMapper
    {
        internal static RefundToCustomerStateDefinition.State MapToState(
            this RefundToCustomerCreated eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent,
            bool isOsno)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
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
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                PatentId = patent?.Id,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static RefundToCustomerStateDefinition.State MapToState(
            this RefundToCustomerUpdated eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract,
            PatentWithoutAdditionalDataDto patent,
            bool isOsno)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Contractor = eventData.Contractor.MapToDefinitionState(),
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
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                PatentId = patent?.Id,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static RefundToCustomerStateDefinition.State MapToState(
            this RefundToCustomerDeleted eventData)
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
