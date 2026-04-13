using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Enums.Extensions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing.BudgetaryPayment
{
    internal static class BudgetaryPaymentMapper
    {
        public static BudgetaryPaymentImportRequest Map(ImportBudgetaryPayment commandData)
        {
            return new BudgetaryPaymentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                AccountCode = commandData.AccountCode,
                KbkPaymentType = commandData.KbkPaymentType,
                KbkId = commandData.KbkId,
                KbkNumber = commandData.KbkNumber,
                Period = BudgetaryPeriodMapper.MapToDomain(commandData.Period),
                PayerStatus = commandData.PayerStatus,
                PaymentBase = commandData.PaymentBase,
                DocumentDate = commandData.DocumentDate,
                DocumentNumber = commandData.DocumentNumber,
                Uin = commandData.Uin,
                Recipient = BudgetaryRecipientMapper.MapToDomain(commandData.Recipient),
                Description = commandData.Description,
                PaymentType = commandData.AccountCode.IsSocialInsurance() ? BudgetaryPaymentType.Other : BudgetaryPaymentType.None,
                TradingObjectId = commandData.AccountCode == BudgetaryAccountCodes.TradingFees ? commandData.TradingObjectId : null,
                PatentId = commandData.PatentId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static BudgetaryPaymentImportRequest Map(ImportDuplicateBudgetaryPayment commandData)
        {
            return new BudgetaryPaymentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                AccountCode = commandData.AccountCode,
                KbkPaymentType = commandData.KbkPaymentType,
                KbkId = commandData.KbkId,
                KbkNumber = commandData.KbkNumber,
                Period = BudgetaryPeriodMapper.MapToDomain(commandData.Period),
                PayerStatus = commandData.PayerStatus,
                PaymentBase = commandData.PaymentBase,
                DocumentDate = commandData.DocumentDate,
                DocumentNumber = commandData.DocumentNumber,
                Uin = commandData.Uin,
                Recipient = BudgetaryRecipientMapper.MapToDomain(commandData.Recipient),
                Description = commandData.Description,
                PaymentType = commandData.AccountCode.IsSocialInsurance() ? BudgetaryPaymentType.Other : BudgetaryPaymentType.None,
                TradingObjectId = commandData.AccountCode == BudgetaryAccountCodes.TradingFees ? commandData.TradingObjectId : null,
                PatentId = commandData.PatentId,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }
    }
}