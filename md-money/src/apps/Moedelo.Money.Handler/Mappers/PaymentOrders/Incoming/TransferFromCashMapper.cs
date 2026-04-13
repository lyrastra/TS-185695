using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class TransferFromCashMapper
    {
        public static TransferFromCashImportRequest Map(ImportTransferFromCash commandData)
        {
            return new TransferFromCashImportRequest
            {
                Date = commandData.Date.Date,
                Sum = commandData.Sum,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static TransferFromCashImportRequest Map(ImportDuplicateTransferFromCash commandData)
        {
            return new TransferFromCashImportRequest
            {
                Date = commandData.Date.Date,
                Sum = commandData.Sum,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
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