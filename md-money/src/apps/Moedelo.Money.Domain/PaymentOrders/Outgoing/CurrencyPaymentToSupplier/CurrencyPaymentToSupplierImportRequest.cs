using System;
using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    public class CurrencyPaymentToSupplierImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        /// <summary>
        /// Сумма платежа в валюте
        /// </summary>
        public decimal Sum { get; set; }
        
        /// <summary>
        /// Сумма платежа в рублях
        /// </summary>
        public decimal TotalSum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public bool IncludeNds { get; set; }

        public NdsType? NdsType { get; set; }

        public decimal? NdsSum { get; set; }

        public KontragentWithRequisites Kontragent{ get; set; }

        public long? ContractBaseId { get; set; }

        /// <summary>
        /// Связанные инвойсы
        /// </summary>
        public IReadOnlyCollection<DocumentLinkSaveRequest> DocumentLinks { get; set; }

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }
        
        public OutsourceState? OutsourceState { get; set; }

        public string SourceFileId { get; set; }

        public int ImportId { get; set; }

        public int? ImportRuleId { get; set; }

        public int? ImportLogId { get; set; }
    }
}