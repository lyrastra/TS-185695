using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee
{
    public class MediationFeeImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public KontragentWithRequisites Kontragent { get; set; }

        public long ContractBaseId { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public IReadOnlyCollection<DocumentLinkSaveRequest> DocumentLinks { get; set; }

        public IReadOnlyCollection<BillLinkSaveRequest> BillLinks { get; set; }

        public long? DuplicateId { get; set; }

        public int? ImportLogId { get; set; }

        public OperationState OperationState { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public int[] ImportRuleIds { get; set; }
        public OutsourceState? OutsourceState { get; set; }
    }
}
