using Moedelo.Money.Domain.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming
{
    internal static class AccrualOfInterestMapper
    {
        public static AccrualOfInterestImportRequest Map(ImportAccrualOfInterest commandData)
        {
            return new AccrualOfInterestImportRequest
            {
                Date = commandData.Date.Date,
                Sum = commandData.Sum,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                TaxationSystemType = commandData.TaxationSystemType,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static AccrualOfInterestImportRequest Map(ImportDuplicateAccrualOfInterest commandData)
        {
            return new AccrualOfInterestImportRequest
            {
                Date = commandData.Date.Date,
                Sum = commandData.Sum,
                Number = commandData.Number,
                Description = commandData.Description,
                SettlementAccountId = commandData.SettlementAccountId,
                TaxationSystemType = commandData.TaxationSystemType,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }
    }
}
