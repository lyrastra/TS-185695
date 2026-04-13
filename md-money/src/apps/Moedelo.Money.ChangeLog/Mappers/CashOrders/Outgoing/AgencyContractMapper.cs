using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.AgencyContract.Events;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing
{
    internal static class AgencyContractMapper
    {
        internal static AgencyContractStateDefinition.State MapToState(
            this AgencyContractCreated eventData,
            CashDto cash,
            ContractDto contract)
        {
            return new AgencyContractStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                Destination = eventData.Destination,
                Comment = eventData.Comment,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static AgencyContractStateDefinition.State MapToState(
            this AgencyContractUpdated eventData,
            CashDto cash,
            ContractDto contract)
        {
            return new AgencyContractStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                ContractorId = eventData.Contractor.Id,
                ContractorName = eventData.Contractor.Name,
                ContractBaseId = eventData.ContractBaseId,
                ContractName = contract?.MapToName(eventData.ContractBaseId),
                Destination = eventData.Destination,
                Comment = eventData.Comment,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderOutgoingPaymentAgencyContract
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static AgencyContractStateDefinition.State MapToState(
            this AgencyContractDeleted eventData)
        {
            return new AgencyContractStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }
    }
}
