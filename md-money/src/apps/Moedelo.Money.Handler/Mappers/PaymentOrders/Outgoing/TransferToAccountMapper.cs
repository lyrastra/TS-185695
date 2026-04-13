using Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    internal static class TransferToAccountMapper
    {
        public static TransferToAccountImportRequest Map(ImportTransferToAccount commandData)
        {
            return new TransferToAccountImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                ToSettlementAccountId = commandData.ToSettlementAccountId,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static TransferToAccountImportRequest Map(ImportDuplicateTransferToAccount commandData)
        {
            return new TransferToAccountImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                ToSettlementAccountId = commandData.ToSettlementAccountId,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                DuplicateId = commandData.DuplicateId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleIds = commandData.ImportRuleIds,
                IsIgnoreNumber = commandData.IsIgnoreNumber,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static TransferToAccountApplyIgnoreNumberRequest Map(ApplyIgnoreNumberTransferToAccount commandData)
        {
            return new TransferToAccountApplyIgnoreNumberRequest
            {
                DocumentBaseIds = commandData.DocumentBaseIds,
                ImportRuleId = commandData.ImportRuleId
            };
        }
    }
}
