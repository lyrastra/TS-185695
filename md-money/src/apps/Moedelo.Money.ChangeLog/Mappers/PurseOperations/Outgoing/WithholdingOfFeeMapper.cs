using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PurseOperations.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.WithholdingOfFee.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PurseOperations.Outgoing
{
    internal static class WithholdingOfFeeMapper
    {
        internal static WithholdingOfFeeStateDefinition.State MapToState(
            this WithholdingOfFeeCreated eventData,
            PurseDto purse,
            PatentWithoutAdditionalDataDto patent)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                PurseId = eventData.PurseId,
                PurseName = purse?.Name,
                Sum = MoneySum.InRubles(eventData.Sum),
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType?.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),

                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static WithholdingOfFeeStateDefinition.State MapToState(
            this WithholdingOfFeeUpdated eventData,
            PurseDto purse,
            PatentWithoutAdditionalDataDto patent)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                PurseId = eventData.PurseId,
                PurseName = purse?.Name,
                Sum = MoneySum.InRubles(eventData.Sum),
                Comment = eventData.Comment,
                TaxationSystemType = eventData.TaxationSystemType?.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                OldOperationType = eventData.OldOperationType != OperationType.PurseOperationComission
                    ? eventData.OldOperationType.GetDescription()
                    : null,

                IsManualTaxPostings = eventData.IsManualTaxPostings,
                ProvideInAccounting = eventData.ProvideInAccounting
            };
        }

        internal static WithholdingOfFeeStateDefinition.State MapToState(
            this WithholdingOfFeeDeleted eventData)
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
