using System;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.RetailRevenue
{
    public class RetailRevenueSaveRequest : IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public long? PatentId { get; set; }

        public decimal? AcquiringCommissionSum { get; set; }

        public DateTime? AcquiringCommissionDate { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }
        
        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public bool ProvideInAccounting { get; set; }

        public TaxPostingsData TaxPostings { get; set; }

        public DateTime SaleDate { get; set; }

        public long? DuplicateId { get; set; }

        public string SourceFileId { get; set; }

        public OperationState OperationState { get; set; }

        public bool IsMediation { get; set; }

        public OutsourceState? OutsourceState { get; set; }

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
