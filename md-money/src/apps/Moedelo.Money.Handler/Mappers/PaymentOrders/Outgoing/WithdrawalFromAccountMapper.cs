using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    internal static class WithdrawalFromAccountMapper
    {
        public static WithdrawalFromAccountImportRequest Map(ImportWithdrawalFromAccount commandData)
        {
            return new WithdrawalFromAccountImportRequest
            {
                Date = commandData.Date.Date,
                Sum = commandData.Sum,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                CashOrderBaseId = commandData.CashOrderBaseId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static WithdrawalFromAccountImportRequest Map(ImportDuplicateWithdrawalFromAccount commandData)
        {
            return new WithdrawalFromAccountImportRequest
            {
                Date = commandData.Date.Date,
                Sum = commandData.Sum,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                CashOrderBaseId = commandData.CashOrderBaseId,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static WithdrawalFromAccountApplyIgnoreNumberRequest Map(ApplyIgnoreNumberWithdrawalFromAccount commandData)
        {
            return new WithdrawalFromAccountApplyIgnoreNumberRequest
            {
                DocumentBaseIds = commandData.DocumentBaseIds,
                ImportRuleId = commandData.ImportRuleId
            };
        }
    }
}
