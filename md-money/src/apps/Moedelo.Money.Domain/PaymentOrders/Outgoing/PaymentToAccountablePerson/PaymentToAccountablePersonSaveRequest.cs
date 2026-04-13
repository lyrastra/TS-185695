using Moedelo.Money.Domain.Payroll;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public class PaymentToAccountablePersonSaveRequest : IActualizableSaveRequest, IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public Employee Employee { get; set; }

        public bool ProvideInAccounting { get; set; }

        public IReadOnlyCollection<long> AdvanceStatementBaseIds { get; set; } = Array.Empty<long>();

        public TaxPostingsData TaxPostings { get; set; }

        public bool IsPaid { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public string SourceFileId { get; set; }

        public bool IsIgnoreNumber { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public bool IsSaveNumeration { get; set; }

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
