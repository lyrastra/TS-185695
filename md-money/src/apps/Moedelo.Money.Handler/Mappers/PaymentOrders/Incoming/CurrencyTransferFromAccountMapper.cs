using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class CurrencyTransferFromAccountMapper
    {
        public static CurrencyTransferFromAccountImportRequest Map(ImportCurrencyTransferFromAccount commandData)
        {
            return new CurrencyTransferFromAccountImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                FromSettlementAccountId = commandData.FromSettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static CurrencyTransferFromAccountImportRequest Map(ImportDuplicateCurrencyTransferFromAccount commandData)
        {
            return new CurrencyTransferFromAccountImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                FromSettlementAccountId = commandData.FromSettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                DuplicateId = commandData.DuplicateId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static CurrencyTransferFromAccountImportRequest Map(ImportWithMissingCurrencySettlementAccountCurrencyTransferFromAccount commandData)
        {
            return new CurrencyTransferFromAccountImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingCurrencySettlementAccount,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }
    }
}