using System;
using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer
{
    public class PaymentFromCustomerSaveRequest : IPaymentOrderOutsourceSaveRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public KontragentWithRequisites Kontragent{ get; set; }

        public long? ContractBaseId { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public bool IncludeNds { get; set; }
        public NdsType? NdsType { get; set; }
        public decimal? NdsSum { get; set; }

        /// <summary>
        /// НДС для посредничества
        /// </summary>
        public NdsType? MediationNdsType { get; set; }
        public decimal? MediationNdsSum { get; set; }

        public bool IsMediation { get; set; }

        public decimal? MediationCommissionSum { get; set; }

        public bool IsMainContractor { get; set; }

        public bool ProvideInAccounting { get; set; }

        public IReadOnlyCollection<BillLinkSaveRequest> BillLinks { get; set; }

        public IReadOnlyCollection<DocumentLinkSaveRequest> DocumentLinks { get; set; }

        public IReadOnlyCollection<InvoiceLinkSaveRequest> InvoiceLinks { get; set; }

        /// <summary>
        /// Резерв (данная величина уменьшает сумму, покрываемую первичными документами)
        /// </summary>
        public decimal? ReserveSum { get; set; }

        public TaxPostingsData TaxPostings { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public long? PatentId { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public string SourceFileId { get; set; }

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
