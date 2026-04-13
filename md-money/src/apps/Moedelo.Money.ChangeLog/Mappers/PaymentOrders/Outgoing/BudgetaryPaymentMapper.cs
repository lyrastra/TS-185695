using System;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Domain.Definitions.Money.PaymentOrders.Outgoing;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.BaseDocuments.Dtos;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Events;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.Enums.SettlementAccounts;

namespace Moedelo.Money.ChangeLog.Mappers.PaymentOrders.Outgoing
{
    internal static class BudgetaryPaymentMapper
    {
        internal static BudgetaryPaymentStateDefinition.State MapToState(
            this BudgetaryPaymentCreated eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            KbkDto kbkInfo)
        {
            return new ()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                Sum = new MoneySum(eventData.Sum, (settlementAccount?.Currency ?? Currency.RUB).ToString()),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                IsPaid = eventData.IsPaid,
                CurrencyInvoicesLinks = eventData.CurrencyInvoicesLinks
                    // todo: узнать, в какой валюте тут сумма (сходить в команду учёта (Козлов Р., Фёдорова К.))
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId), "RUB"))
                    .ToArray(),
                KbkId = eventData.KbkId,
                KbkNumber = kbkInfo.GetFullKbkName() ?? eventData.KbkNumber,
                KbkPaymentType = eventData.KbkPaymentType.Map(eventData.AccountCode),
                AccountType = eventData.AccountCode.GetAccountType(),
                AccountCode = eventData.AccountCode.GetDescription(),
                Period = eventData.Period.ToStringPeriodValue(),
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                PayerStatus = eventData.PayerStatus.GetDescription(),
                PaymentBase = eventData.PaymentBase.GetDescription(),
                DocumentNumber = eventData.DocumentNumber,
                DocumentDate = eventData.DocumentDate.FormatDate109(),
                Uin = eventData.Uin,
                Recipient = eventData.Recipient.MapRecipient()
            };
        }

        internal static BudgetaryPaymentStateDefinition.State MapToState(
            this BudgetaryPaymentUpdated eventData,
            SettlementAccountDto settlementAccount,
            IReadOnlyDictionary<long, BaseDocumentDto> linkedDocuments,
            KbkDto kbkInfo)
        {
            return new()
            {
                DocumentBaseId = eventData.DocumentBaseId,
                Date = eventData.Date,
                Number = eventData.Number,
                Sum = new MoneySum(eventData.Sum, (settlementAccount?.Currency ?? Currency.RUB).ToString()),
                Description = eventData.Description,
                ProvideInAccounting = eventData.ProvideInAccounting,
                IsManualTaxPostings = eventData.IsManualTaxPostings,
                IsPaid = eventData.IsPaid,
                CurrencyInvoicesLinks = eventData.CurrencyInvoicesLinks
                    // todo: узнать, в какой валюте тут сумма (сходить в команду учёта (Козлов Р., Фёдорова К.))
                    .Select(doc => doc.MapToDefinitionState(linkedDocuments.GetValueOrDefault(doc.DocumentBaseId), "RUB"))
                    .ToArray(),
                KbkId = eventData.KbkId,
                KbkNumber = kbkInfo.GetFullKbkName() ?? eventData.KbkNumber,
                KbkPaymentType = eventData.KbkPaymentType.Map(eventData.AccountCode),
                AccountType = eventData.AccountCode.GetAccountType(),
                AccountCode = eventData.AccountCode.GetDescription(),
                Period = eventData.Period.ToStringPeriodValue(),
                SettlementAccountId = eventData.SettlementAccountId,
                SettlementAccountNumber = settlementAccount?.Number,
                PayerStatus = eventData.PayerStatus.GetDescription(),
                PaymentBase = eventData.PaymentBase.GetDescription(),
                DocumentNumber = eventData.DocumentNumber,
                DocumentDate = eventData.DocumentDate.FormatDate109(),
                Uin = eventData.Uin,
                Recipient = eventData.Recipient.MapRecipient()
            };
        }

        internal static BudgetaryPaymentStateDefinition.State MapToState(
            this BudgetaryPaymentDeleted eventData)
        {
            return new()
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


        private static BudgetaryPaymentStateDefinition.State.PaymentRecipient MapRecipient(
            this BudgetaryPaymentRecipient recipient)
        {
            return new()
            {
                Name = recipient.Name,
                Inn = recipient.Inn,
                Kpp = recipient.Kpp,
                Okato = recipient.Okato,
                Oktmo = recipient.Oktmo,
                BankBik = recipient.BankBik,
                BankName = recipient.BankName,
                SettlementAccount = recipient.SettlementAccount,
                BankCorrespondentAccount = recipient.BankCorrespondentAccount
            };
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
