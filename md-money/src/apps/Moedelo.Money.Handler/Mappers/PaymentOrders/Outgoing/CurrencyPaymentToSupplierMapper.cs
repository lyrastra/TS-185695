using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    internal static class CurrencyPaymentToSupplierMapper
    {
        public static CurrencyPaymentToSupplierImportRequest Map(ImportCurrencyPaymentToSupplier commandData)
        {
            return new CurrencyPaymentToSupplierImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                TotalSum = commandData.TotalSum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static CurrencyPaymentToSupplierImportRequest Map(ImportDuplicateCurrencyPaymentToSupplier commandData)
        {
            return new CurrencyPaymentToSupplierImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                TotalSum = commandData.TotalSum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                DuplicateId = commandData.DuplicateId,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static CurrencyPaymentToSupplierImportRequest Map(ImportWithMissingMissingContractorCurrencyPaymentToSupplier commandData)
        {
            return new CurrencyPaymentToSupplierImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                TotalSum = commandData.TotalSum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingKontragent,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static CurrencyPaymentToSupplierImportRequest Map(ImportWithMissingMissingExchangeRateCurrencyPaymentToSupplier commandData)
        {
            return new CurrencyPaymentToSupplierImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
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
