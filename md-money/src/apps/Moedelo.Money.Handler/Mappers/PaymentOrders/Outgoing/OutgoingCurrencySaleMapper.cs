using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    public class OutgoingCurrencySaleMapper
    {
        public static OutgoingCurrencySaleImportRequest Map(ImportOutgoingCurrencySale commandData)
        {
            return new OutgoingCurrencySaleImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                TotalSum = commandData.TotalSum,
                ExchangeRate = commandData.ExchangeRate,
                ExchangeRateDiff = commandData.ExchangeRateDiff,
                ToSettlementAccountId = commandData.ToSettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static OutgoingCurrencySaleImportRequest Map(ImportDuplicateOutgoingCurrencySale commandData)
        {
            return new OutgoingCurrencySaleImportRequest
            {
                Sum = commandData.Sum,
                Date = commandData.Date,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                TotalSum = commandData.TotalSum,
                ExchangeRate = commandData.ExchangeRate,
                ExchangeRateDiff = commandData.ExchangeRateDiff,
                ToSettlementAccountId = commandData.ToSettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                DuplicateId = commandData.DuplicateId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static OutgoingCurrencySaleImportRequest Map(ImportWithMissingMissingExchangeRateOutgoingCurrencySale commandData)
        {
            return new OutgoingCurrencySaleImportRequest
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
    }
}