using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    public class WithdrawalFromAccountImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public long? CashOrderBaseId { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public int[] ImportRuleIds { get; set; }

        public bool IsIgnoreNumber { get; set; }

        public int? ImportLogId { get; set; }
    }
}
