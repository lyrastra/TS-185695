using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Events;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Models;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Infrastructure.Json;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.Common;
using Moedelo.Money.Enums.Extensions;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class UnifiedBudgetaryPaymentMapper
    {
        internal static UnifiedBudgetaryPaymentStateDefinition.State MapToState(
            this UnifiedBudgetaryPaymentCreated eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<int, KbkDto> kbkMap,
            IReadOnlyDictionary<long, PatentWithoutAdditionalDataDto> patentMap)
        {
            return new UnifiedBudgetaryPaymentStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Recipient = eventData.Recipient.ToJsonString(),
                SubPayments = MapToState(eventData.SubPayments, kbkMap, patentMap)
            };
        }

        internal static UnifiedBudgetaryPaymentStateDefinition.State MapToState(
            this UnifiedBudgetaryPaymentUpdated eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<int, KbkDto> kbkMap,
            IReadOnlyDictionary<long, PatentWithoutAdditionalDataDto> patentMap)
        {
            return new UnifiedBudgetaryPaymentStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount.Number,
                Sum = MoneySum.InRubles(eventData.Sum),
                Recipient = eventData.Recipient.ToJsonString(),
                SubPayments = MapToState(eventData.SubPayments, kbkMap, patentMap)
            };
        }

        internal static UnifiedBudgetaryPaymentStateDefinition.State MapToState(
            this UnifiedBudgetaryPaymentDeleted eventData)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }

        private static UnifiedBudgetarySubPaymentInfo[] MapToState(
            IReadOnlyCollection<UnifiedBudgetarySubPayment> subPayments,
            IReadOnlyDictionary<int, KbkDto> kbkMap,
            IReadOnlyDictionary<long, PatentWithoutAdditionalDataDto> patentMap)
        {
            return subPayments
                .Select(x => MapToState(x, kbkMap, patentMap))
                .ToArray();
        }

        private static UnifiedBudgetarySubPaymentInfo MapToState(
            UnifiedBudgetarySubPayment subPayment,
            IReadOnlyDictionary<int, KbkDto> kbkMap,
            IReadOnlyDictionary<long, PatentWithoutAdditionalDataDto> patentMap)
        {
            var kbk = kbkMap.GetValueOrDefault(subPayment.KbkId);
            var patent = subPayment.PatentId.HasValue
                ? patentMap.GetValueOrDefault(subPayment.PatentId.Value)
                : null;
            return new UnifiedBudgetarySubPaymentInfo
            {
                DocumentBaseId = subPayment.DocumentBaseId,
                Sum = MoneySum.InRubles(subPayment.Sum),
                KbkId = subPayment.KbkId,
                KbkNumber = kbk?.GetFullKbkName(),
                KbkPaymentType = kbk.MapToKbkPaymentType(),
                AccountType = ((BudgetaryAccountCodes?)kbk?.AccountCode)?.GetAccountType() ?? 0,
                AccountCode = ((BudgetaryAccountCodes?)kbk?.AccountCode)?.GetDescription(),
                Period = subPayment.Period.ToStringPeriodValue(),
                PatentName = subPayment.PatentId.HasValue
                    ? patent.MapToName(subPayment.PatentId.Value)
                    : null,
                PatentId = subPayment.PatentId,
                IsManualTaxPostings = subPayment.IsManualTaxPostings,
            };
        }

        private static UnifiedBudgetarySubPaymentInfo.KbkPaymentTypeEnum MapToKbkPaymentType(this KbkDto kbk)
        {
            if (kbk == null)
            {
                return 0;
            }
            return kbk.KbkPaymentType switch
            {
                Catalog.ApiClient.Enums.KbkPaymentType.Payment =>
                    ((BudgetaryAccountCodes)kbk.AccountCode).GetAccountType() == UnifiedBudgetarySubPaymentInfo.BudgetaryAccountType.Fees
                    ? UnifiedBudgetarySubPaymentInfo.KbkPaymentTypeEnum.PaymentFees
                    : UnifiedBudgetarySubPaymentInfo.KbkPaymentTypeEnum.PaymentTaxes,
                Catalog.ApiClient.Enums.KbkPaymentType.Surcharge => UnifiedBudgetarySubPaymentInfo.KbkPaymentTypeEnum.Surcharge,
                Catalog.ApiClient.Enums.KbkPaymentType.Forfeit => UnifiedBudgetarySubPaymentInfo.KbkPaymentTypeEnum.Forfeit,
                _ => throw new ArgumentOutOfRangeException(nameof(kbk.KbkPaymentType), kbk.KbkPaymentType, null)
            };
        }

        private static UnifiedBudgetarySubPaymentInfo.BudgetaryAccountType GetAccountType(
            this BudgetaryAccountCodes accountCode)
        {
            return accountCode.IsSocialInsurance()
                ? UnifiedBudgetarySubPaymentInfo.BudgetaryAccountType.Fees
                : UnifiedBudgetarySubPaymentInfo.BudgetaryAccountType.Taxes;
        }
    }
}
