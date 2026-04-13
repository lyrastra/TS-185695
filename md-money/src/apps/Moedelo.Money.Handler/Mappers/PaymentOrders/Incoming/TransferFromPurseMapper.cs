using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class TransferFromPurseMapper
    {
        public static TransferFromPurseImportRequest Map(ImportTransferFromPurse commandData)
        {
            return new TransferFromPurseImportRequest
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

        public static TransferFromPurseImportRequest Map(ImportDuplicateTransferFromPurse commandData)
        {
            return new TransferFromPurseImportRequest
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