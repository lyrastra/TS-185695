using Moedelo.Money.Domain;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson.Commands;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Incoming;

internal static class RefundFromAccountablePersonMapper
{
    public static RefundFromAccountablePersonImportRequest Map(ImportRefundFromAccountablePerson commandData)
        {
            return new RefundFromAccountablePersonImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                EmployeeId = commandData.Employee.Id,
                EmployeeName = commandData.Employee.Name,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
            };
        }

        public static RefundFromAccountablePersonImportRequest Map(ImportDuplicateRefundFromAccountablePerson commandData)
        {
            return new RefundFromAccountablePersonImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                EmployeeId = commandData.Employee.Id,
                EmployeeName = commandData.Employee.Name,
                Description = commandData.Description,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static RefundFromAccountablePersonImportRequest Map(ImportWithMissingEmployeeRefundFromAccountablePerson commandData)
        {
            return new RefundFromAccountablePersonImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                Sum = commandData.Sum,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = commandData.Description,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingWorker,
                ImportRuleIds = commandData.ImportRuleIds,
                ImportLogId = commandData.ImportLogId,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                MissingWorkerRequisites = new MissingWorkerRequisitesSaveRequest
                {
                    Name = commandData.PayerName,
                    Inn = commandData.PayerInn,
                    SettlementNumber = commandData.PayerAccount
                }
            };
        }
}