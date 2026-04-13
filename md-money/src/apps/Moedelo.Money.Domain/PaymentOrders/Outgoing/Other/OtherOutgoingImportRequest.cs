using Moedelo.Money.Domain.AccPostings;
using Moedelo.Money.Enums;
using System;
using Moedelo.Money.Domain.TaxPostings;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.Other
{
    public class OtherOutgoingImportRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public ContractorWithRequisites Contractor { get; set; }

        public long? ContractBaseId { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        /// <summary>
        /// Бухгалтерская проводка, она создаётся в импортированном платеже
        /// </summary>
        public OtherOutgoingCustomAccPosting AccPosting { get; set; }
        public ImportCustomTaxPosting TaxPosting { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public OutsourceState? OutsourceState { get; set; }

        public int? ImportLogId { get; set; }
        
        public int[] ImportRuleIds { get; set; }
    }
}
