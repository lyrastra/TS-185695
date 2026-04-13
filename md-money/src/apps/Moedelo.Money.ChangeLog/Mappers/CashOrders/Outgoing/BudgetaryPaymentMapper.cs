using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.CashOrders.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.BudgetaryPayment.Events;
using System;

namespace Moedelo.Money.ChangeLog.Mappers.CashOrders.Outgoing
{
    internal static class BudgetaryPaymentMapper
    {
        internal static BudgetaryPaymentStateDefinition.State MapToState(
            this BudgetaryPaymentCreated eventData,
            CashDto cash,
            KbkDto kbkInfo)
        {
            return new BudgetaryPaymentStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                KbkId = eventData.KbkId,
                KbkNumber = kbkInfo.GetFullKbkName() ?? eventData.KbkNumber,
                KbkPaymentType = eventData.KbkPaymentType.Map(eventData.AccountCode),
                AccountType = eventData.AccountCode.GetAccountType(),
                AccountCode = eventData.AccountCode.GetDescription(),
                Period = eventData.Period.ToStringPeriodValue(),
                Destination = eventData.Destination,
                Recipient = eventData.Recipient
            };
        }

        internal static BudgetaryPaymentStateDefinition.State MapToState(
            this BudgetaryPaymentUpdated eventData,
            CashDto cash,
            KbkDto kbkInfo)
        {
            return new BudgetaryPaymentStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                CashId = eventData.CashId,
                CashName = cash.MapToName(eventData.CashId),
                Sum = MoneySum.InRubles(eventData.Sum),
                KbkId = eventData.KbkId,
                KbkNumber = kbkInfo.GetFullKbkName() ?? eventData.KbkNumber,
                KbkPaymentType = eventData.KbkPaymentType.Map(eventData.AccountCode),
                AccountType = eventData.AccountCode.GetAccountType(),
                AccountCode = eventData.AccountCode.GetDescription(),
                Period = eventData.Period.ToStringPeriodValue(),
                Destination = eventData.Destination,
                Recipient = eventData.Recipient,
                OldOperationType = eventData.OldOperationType != OperationType.CashOrderBudgetaryPayment
                    ? eventData.OldOperationType.GetDescription()
                    : null
            };
        }

        internal static BudgetaryPaymentStateDefinition.State MapToState(
            this BudgetaryPaymentDeleted eventData)
        {
            return new BudgetaryPaymentStateDefinition.State
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number
            };
        }

        private static BudgetaryPaymentStateDefinition.State.BudgetaryAccountType GetAccountType(this BudgetaryAccountCodes accountCode)
        {
            return accountCode.IsSocialInsurance()
                ? BudgetaryPaymentStateDefinition.State.BudgetaryAccountType.Fees
                : BudgetaryPaymentStateDefinition.State.BudgetaryAccountType.Taxes;
        }

        private static BudgetaryPaymentStateDefinition.State.KbkPaymentTypeEnum Map(
            this KbkPaymentType value,
            BudgetaryAccountCodes accountCode)
        {
            return value switch
            {
                KbkPaymentType.Payment =>
                    accountCode.GetAccountType() == BudgetaryPaymentStateDefinition.State.BudgetaryAccountType.Fees
                    ? BudgetaryPaymentStateDefinition.State.KbkPaymentTypeEnum.PaymentFees
                    : BudgetaryPaymentStateDefinition.State.KbkPaymentTypeEnum.PaymentTaxes,
                KbkPaymentType.Surcharge => BudgetaryPaymentStateDefinition.State.KbkPaymentTypeEnum.Surcharge,
                KbkPaymentType.Forfeit => BudgetaryPaymentStateDefinition.State.KbkPaymentTypeEnum.Forfeit,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
            };
        }
    }
}
