using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    internal static class UnifiedBudgetaryPaymentMapper
    {
        public static UnifiedBudgetaryPaymentImportRequest Map(ImportUnifiedBudgetaryPayment commandData)
        {
            return new UnifiedBudgetaryPaymentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Uin = commandData.Uin,
                Recipient = UnifiedBudgetaryRecipientMapper.MapToDomain(commandData.Recipient),
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                PayerStatus = commandData.PayerStatus,
            };
        }

        public static UnifiedBudgetaryPaymentImportRequest Map(ImportDuplicateUnifiedBudgetaryPayment commandData)
        {
            return new UnifiedBudgetaryPaymentImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Uin = commandData.Uin,
                Recipient = UnifiedBudgetaryRecipientMapper.MapToDomain(commandData.Recipient),
                Description = commandData.Description,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                PayerStatus = commandData.PayerStatus,
            };
        }
    }
}