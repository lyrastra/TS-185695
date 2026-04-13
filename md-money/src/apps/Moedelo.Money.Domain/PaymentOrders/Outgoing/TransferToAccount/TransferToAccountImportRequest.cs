using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.TransferToAccount
{
    public class TransferToAccountImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int SettlementAccountId { get; set; }

        public int ToSettlementAccountId { get; set; }

        public string Description { get; set; }

        public OperationState OperationState { get; set; }

        public long? DuplicateId { get; set; }

        public string SourceFileId { get; set; }

		public int ImportId { get; set; }

        public int[] ImportRuleIds { get; set; }

        public bool IsIgnoreNumber { get; set; }

        public int? ImportLogId { get; set; }
        public OutsourceState? OutsourceState { get; set; }
    }
}
