using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class OtherOutgoingMapper
    {
        internal static OtherOutgoingStateDefinition.State MapToState(
            this OtherOutgoingCreated eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract)
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
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static OtherOutgoingStateDefinition.State MapToState(
            this OtherOutgoingUpdated eventData,
            SettlementAccountDto settlementAccount,
            ContractDto contract)
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
                WithNds = eventData.Nds.IsWithNds(),
                NdsType = eventData.Nds.GetNdsType(),
                NdsSum = eventData.Nds.GetNdsSum(),
                Description = eventData.Description,
                IsPaid = eventData.IsPaid,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static OtherOutgoingStateDefinition.State MapToState(
            this OtherOutgoingDeleted eventData)
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
