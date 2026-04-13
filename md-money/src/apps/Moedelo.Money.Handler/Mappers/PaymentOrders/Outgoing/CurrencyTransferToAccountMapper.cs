using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    internal static class CurrencyTransferToAccountMapper
    {
        public static CurrencyTransferToAccountImportRequest Map(ImportCurrencyTransferToAccount commandData)
        {
            return new CurrencyTransferToAccountImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                ToSettlementAccountId = commandData.ToSettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static CurrencyTransferToAccountImportRequest Map(ImportDuplicateCurrencyTransferToAccount commandData)
        {
            return new CurrencyTransferToAccountImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                ToSettlementAccountId = commandData.ToSettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                DuplicateId = commandData.DuplicateId,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static CurrencyTransferToAccountImportRequest Map(ImportWithMissingCurrencySettlementAccountCurrencyTransferToAccount commandData)
        {
            return new CurrencyTransferToAccountImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingCurrencySettlementAccount,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }
    }
}
