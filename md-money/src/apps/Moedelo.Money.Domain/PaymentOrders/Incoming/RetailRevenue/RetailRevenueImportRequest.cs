using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue
{
    public class RetailRevenueImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal? AcquiringCommissionSum { get; set; }

        public DateTime? AcquiringCommissionDate { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? DuplicateId { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public OperationState OperationState { get; set; }

        public int[] ImportRuleIds { get; set; }

        public bool IsMediation { get; set; }

        public int? ImportLogId { get; set; }

        public OutsourceState? OutsourceState { get; set; }
        
        /// <summary>
        /// НДС
        /// </summary>
        public bool IncludeNds { get; set; }
        public NdsType? NdsType { get; set; }
        public decimal? NdsSum { get; set; }
    }
}
