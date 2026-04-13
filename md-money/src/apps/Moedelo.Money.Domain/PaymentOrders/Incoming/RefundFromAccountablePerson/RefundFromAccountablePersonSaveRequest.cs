using Moedelo.Money.Enums;
using System;
using Moedelo.Money.Domain.Payroll;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    public class RefundFromAccountablePersonSaveRequest : IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Employee Employee { get; set; }

        public long? AdvanceStatementBaseId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public string SourceFileId { get; set; }

        public MissingWorkerRequisitesSaveRequest MissingWorkerRequisites { get; set; }

        /// <summary>
        /// Идентификаторы применённых правил импорта
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        /// <summary>
        /// Идентификатор лога импорта
        /// </summary>
        public int? ImportLogId { get; set; }
    }
}
