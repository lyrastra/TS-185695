using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class CurrencyPaymentFromCustomerMapper
    {
        public static CurrencyPaymentFromCustomerImportRequest Map(ImportCurrencyPaymentFromCustomer commandData)
        {
            return new CurrencyPaymentFromCustomerImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                TotalSum = commandData.TotalSum,
                Description = commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                SettlementAccountId = commandData.SettlementAccountId,
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


        public static CurrencyPaymentFromCustomerImportRequest Map(ImportDuplicateCurrencyPaymentFromCustomer commandData)
        {
            return new CurrencyPaymentFromCustomerImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                TotalSum = commandData.TotalSum,
                Description = commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                SettlementAccountId = commandData.SettlementAccountId,
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

        public static CurrencyPaymentFromCustomerImportRequest Map(ImportWithMissingContractorCurrencyPaymentFromCustomer commandData)
        {
            return new CurrencyPaymentFromCustomerImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                TotalSum = commandData.TotalSum,
                Description = commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                SettlementAccountId = commandData.SettlementAccountId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingKontragent,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static CurrencyPaymentFromCustomerImportRequest Map(ImportWithMissingExchangeRateCurrencyPaymentFromCustomer commandData)
        {
            return new CurrencyPaymentFromCustomerImportRequest
            {
                Date = commandData.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                Description = commandData.Description,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
                SettlementAccountId = commandData.SettlementAccountId,
                Kontragent = KontragentMapper.MapToKontragent(commandData.Contractor),
                ContractBaseId = commandData.ContractBaseId,
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