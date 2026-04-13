using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment
{
    public class LoanRepaymentImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public KontragentWithRequisites Kontragent { get; set; }

        public long ContractBaseId { get; set; }

        /// <summary>
        /// Признак: долгосрочный займ или кредит
        /// </summary>
        public bool IsLongTermLoan { get; set; }

        /// <summary>
        /// В т.ч. проценты
        /// </summary>
        public decimal? LoanInterestSum { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public int? ImportRuleId { get; set; }

        public int? ImportLogId { get; set; }
        public OutsourceState? OutsourceState { get; set; }
    }
}
