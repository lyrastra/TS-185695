using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public class PaymentToAccountablePersonImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public int? EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public int[] ImportRuleIds { get; set; }

        public bool IsIgnoreNumber { get; set; }

        /// <summary>
        /// ИНН получателя платежа
        /// </summary>
        public string PayeeInn { get; set; }

        /// <summary>
        /// Счёт получателя платежа
        /// </summary>
        public string PayeeAccount { get; set; }

        /// <summary>
        /// ФИО получателя платежа
        /// </summary>
        public string PayeeName { get; set; }

        public int? ImportLogId { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}
