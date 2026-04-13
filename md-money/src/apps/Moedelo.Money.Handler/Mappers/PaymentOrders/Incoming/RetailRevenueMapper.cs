using Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class RetailRevenueMapper
    {
        public static RetailRevenueImportRequest Map(ImportRetailRevenue commandData)
        {
            return new RetailRevenueImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = commandData.Description,
                AcquiringCommissionSum = commandData.AcquiringCommissionSum,
                AcquiringCommissionDate = commandData.AcquiringCommissionDate,
                TaxationSystemType = commandData.TaxationSystemType,
                IsMediation = commandData.IsMediation,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum
            };
        }

        public static RetailRevenueImportRequest Map(ImportDuplicateRetailRevenue commandData)
        {
            return new RetailRevenueImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = commandData.Description,
                AcquiringCommissionSum = commandData.AcquiringCommissionSum,
                AcquiringCommissionDate = commandData.AcquiringCommissionDate,
                TaxationSystemType = commandData.TaxationSystemType,
                IsMediation = commandData.IsMediation,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId,
                IncludeNds = commandData.Nds != null,
                NdsType = commandData.Nds?.NdsType,
                NdsSum = commandData.Nds?.NdsSum,
            };
        }
    }
}
