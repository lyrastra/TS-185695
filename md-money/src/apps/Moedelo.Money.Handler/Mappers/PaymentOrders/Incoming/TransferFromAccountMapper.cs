using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class TransferFromAccountMapper
    {
        public static TransferFromAccountImportRequest Map(ImportTransferFromAccount commandData)
        {
            return new TransferFromAccountImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                FromSettlementAccountId = commandData.FromSettlementAccountId,
                SettlementAccountId = commandData.SettlementAccountId,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static TransferFromAccountImportRequest Map(ImportDuplicateTransferFromAccount commandData)
        {
            return new TransferFromAccountImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                FromSettlementAccountId = commandData.FromSettlementAccountId,
                SettlementAccountId = commandData.SettlementAccountId,
                Sum = commandData.Sum,
                Description = commandData.Description,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }
        
        public static TransferFromAccountImportRequest Map(ImportAmbiguousOperationTypeTransferFromAccount commandData)
        {
            return new TransferFromAccountImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                FromSettlementAccountId = commandData.FromSettlementAccountId,
                SettlementAccountId = commandData.SettlementAccountId,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.AmbiguousOperationType,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }
    }
}