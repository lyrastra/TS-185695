using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.Other
{
    public class OtherIncomingSaveRequest : IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public ContractorWithRequisites Contractor { get; set; }

        public long? ContractBaseId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public IReadOnlyCollection<BillLinkSaveRequest> BillLinks { get; set; }

        public TaxPostingsData TaxPostings { get; set; }

        public OtherIncomingCustomAccPosting AccPosting { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public long? DuplicateId { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        public OperationState OperationState { get; set; }

        public string SourceFileId { get; set; }

        public bool NonRemovableAutomatic { get; set; }

        public bool IsTargetIncome { get; set; }

        public OutsourceState? OutsourceState { get; set; }
        public bool IsOutsourceImportRuleApplied { get; set; }

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
