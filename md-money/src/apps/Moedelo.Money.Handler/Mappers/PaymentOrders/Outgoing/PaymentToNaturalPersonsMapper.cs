using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Commands;
using Moedelo.Money.Resources;

namespace Moedelo.Money.Handler.Mappers.PaymentOrders.Outgoing
{
    internal static class PaymentToNaturalPersonsMapper
    {
        public static PaymentToNaturalPersonsImportRequest Map(ImportPaymentToNaturalPersons commandData)
        {
            return new PaymentToNaturalPersonsImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = string.IsNullOrWhiteSpace(commandData.Description)
                    ? PaymentOrdersDescriptions.PaymentToNaturalPersons
                    : commandData.Description,
                EmployeeId = commandData.Employee.Id,
                EmployeeName = commandData.Employee.Name,
                PaymentSum = commandData.PaymentSum,
                PaymentType = commandData.PaymentType,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Imported,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static PaymentToNaturalPersonsImportRequest Map(ImportDuplicatePaymentToNaturalPersons commandData)
        {
            return new PaymentToNaturalPersonsImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = string.IsNullOrWhiteSpace(commandData.Description)
                    ? PaymentOrdersDescriptions.PaymentToNaturalPersons
                    : commandData.Description,
                EmployeeId = commandData.Employee.Id,
                EmployeeName = commandData.Employee.Name,
                PaymentSum = commandData.PaymentSum,
                PaymentType = commandData.PaymentType,
                DuplicateId = commandData.DuplicateId,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.Duplicate,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                ImportLogId = commandData.ImportLogId
            };
        }

        public static PaymentToNaturalPersonsImportRequest Map(ImportWithMissingEmployeePaymentToNaturalPersons commandData)
        {
            return new PaymentToNaturalPersonsImportRequest
            {
                Date = commandData.Date.Date,
                Number = commandData.Number,
                SettlementAccountId = commandData.SettlementAccountId,
                Description = string.IsNullOrWhiteSpace(commandData.Description)
                    ? PaymentOrdersDescriptions.PaymentToNaturalPersons
                    : commandData.Description,
                PaymentSum = commandData.PaymentSum,
                PaymentType = commandData.PaymentType,
                SourceFileId = commandData.SourceFileId,
				ImportId = commandData.ImportId,
                OperationState = OperationState.MissingWorker,
                OutsourceState = commandData.NeedOutsourceProcessing ? OutsourceState.Unconfirmed : null,
                ImportRuleId = commandData.ImportRuleId,
                PayeeInn = commandData.PayeeInn,
                PayeeName = commandData.PayeeName,
                PayeeAccount = commandData.PayeeAccount,
                ImportLogId = commandData.ImportLogId
            };
        }
    }
}
