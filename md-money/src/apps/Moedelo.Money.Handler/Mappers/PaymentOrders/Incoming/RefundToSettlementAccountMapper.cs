using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundToSettlementAccount;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundToSettlementAccount.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    public static class RefundToSettlementAccountMapper
    {
        public static ImportRefundToSettlementAccountRequest Map(ImportRefundToSettlementAccount commandData)
        {
            return new ImportRefundToSettlementAccountRequest()
            {
                DocumentBaseId = commandData.DocumentBaseId,
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                Contractor = KontragentMapper.MapToContractor(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                SourceFileId = commandData.SourceFileId,
                ImportId = commandData.ImportId,
                ImportRuleIds = commandData.ImportRuleIds,
                OperationState = OperationState.Imported,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static ImportRefundToSettlementAccountRequest Map(ImportRefundToSettlementAccountDuplicate commandData)
        {
            return new ImportRefundToSettlementAccountRequest()
            {
                DocumentBaseId = commandData.DocumentBaseId,
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                Contractor = KontragentMapper.MapToContractor(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                SourceFileId = commandData.SourceFileId,
                ImportId = commandData.ImportId,
                ImportRuleIds = commandData.ImportRuleIds,
                DuplicateId = commandData.DuplicateId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                OperationState = OperationState.Duplicate,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static ImportRefundToSettlementAccountRequest Map(ImportRefundToSettlementAccountWithMissingContragent commandData)
        {
            return new ImportRefundToSettlementAccountRequest()
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                Contractor = null,
                ContractBaseId = null,
                SourceFileId = commandData.SourceFileId,
                ImportId = commandData.ImportId,
                ImportRuleIds = commandData.ImportRuleIds,
                OperationState = OperationState.MissingKontragent,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }
    }
}
