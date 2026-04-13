using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPurchase.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class IncomingCurrencyPurchaseMapper
    {
        public static IncomingCurrencyPurchaseImportRequest Map(ImportIncomingCurrencyPurchase commandData)
        {
            return new IncomingCurrencyPurchaseImportRequest
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
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static IncomingCurrencyPurchaseImportRequest Map(ImportDuplicateIncomingCurrencyPurchase commandData)
        {
            return new IncomingCurrencyPurchaseImportRequest
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
    }
}