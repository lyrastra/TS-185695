using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BankFee.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class BankFeeMapper
    {
        internal static BankFeeStateDefinition.State MapToState(
            this BankFeeCreated eventData,
            SettlementAccountDto settlementAccount,
            PatentWithoutAdditionalDataDto patent)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                PatentId = patent?.Id,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static BankFeeStateDefinition.State MapToState(
            this BankFeeUpdated eventData,
            SettlementAccountDto settlementAccount,
            PatentWithoutAdditionalDataDto patent)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Number = eventData.Number,
                Date = eventData.Date,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Description = eventData.Description,
                TaxationSystemType = eventData.TaxationSystemType.GetDescription(),
                PatentName = patent.MapToName(eventData.PatentId),
                PatentId = patent?.Id,

                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings
            };
        }

        internal static BankFeeStateDefinition.State MapToState(
            this BankFeeDeleted eventData)
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
