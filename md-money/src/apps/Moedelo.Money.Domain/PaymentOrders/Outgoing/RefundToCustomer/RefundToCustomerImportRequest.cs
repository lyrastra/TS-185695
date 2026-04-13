using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer
{
    public class RefundToCustomerImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public KontragentWithRequisites Kontragent { get; set; }

        public long? ContractBaseId { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public int? ImportRuleId { get; set; }

        public int? ImportLogId { get; set; }
    }
}
