using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    internal static class OutgoingCurrencyPurchaseMapper
    {
        public static OutgoingCurrencyPurchaseImportRequest Map(ImportOutgoingCurrencyPurchase commandData)
        {
            return new OutgoingCurrencyPurchaseImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                ToSettlementAccountId = commandData.ToSettlementAccountId,
                TotalSum = commandData.TotalSum,
                ExchangeRate = commandData.ExchangeRate,
                ExchangeRateDiff = commandData.ExchangeRateDiff,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static OutgoingCurrencyPurchaseImportRequest Map(ImportDuplicateOutgoingCurrencyPurchase commandData)
        {
            return new OutgoingCurrencyPurchaseImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                ToSettlementAccountId = commandData.ToSettlementAccountId,
                TotalSum = commandData.TotalSum,
                ExchangeRate = commandData.ExchangeRate,
                ExchangeRateDiff = commandData.ExchangeRateDiff,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                DuplicateId = commandData.DuplicateId,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static OutgoingCurrencyPurchaseImportRequest Map(ImportWithMissingMissingExchangeRateOutgoingCurrencyPurchase commandData)
        {
            return new OutgoingCurrencyPurchaseImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                ToSettlementAccountId = commandData.ToSettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingExchangeRate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static OutgoingCurrencyPurchaseImportRequest Map(ImportWithMissingCurrencySettlementAccountOutgoingCurrencyPurchase commandData)
        {
            return new OutgoingCurrencyPurchaseImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                TotalSum = commandData.TotalSum,
                ExchangeRate = commandData.ExchangeRate,
                ExchangeRateDiff = commandData.ExchangeRateDiff,
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