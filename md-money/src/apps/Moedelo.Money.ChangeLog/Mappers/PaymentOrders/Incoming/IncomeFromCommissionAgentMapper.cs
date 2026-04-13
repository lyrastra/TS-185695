using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Incoming;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Incoming
{
    internal static class IncomeFromCommissionAgentMapper
    {
        internal static IncomeFromCommissionAgentStateDefinition.State MapToState(
            this IncomeFromCommissionAgentCreated eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract,
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

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static IncomeFromCommissionAgentStateDefinition.State MapToState(
            this IncomeFromCommissionAgentUpdated eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract,
            bool isOsno)
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

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static IncomeFromCommissionAgentStateDefinition.State MapToState(
            this IncomeFromCommissionAgentDeleted eventData)
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
