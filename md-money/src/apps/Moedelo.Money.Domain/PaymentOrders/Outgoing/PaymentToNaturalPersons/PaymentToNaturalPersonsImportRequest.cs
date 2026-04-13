using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public class PaymentToNaturalPersonsImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public int? EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public decimal PaymentSum { get; set; }

        public PaymentToNaturalPersonsType PaymentType { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public int? ImportRuleId { get; set; }

        public string PayeeInn { get; set; }

        public string PayeeAccount { get; set; }

        public string PayeeName { get; set; }

        public int? ImportLogId { get; set; }
    }
}
