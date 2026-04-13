using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using System;
using System.Collections.Generic;
using Moedelo.Money.Providing.Business.Abstractions.Enums;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Models
{
    internal class PaymentLinksCreateRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public decimal Sum { get; set; }

        public IDictionary<long, BaseDocument> BaseDocuments { get; set; }

        public IReadOnlyCollection<BillLink> BillLinks { get; set; } = Array.Empty<BillLink>();

        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; } = Array.Empty<DocumentLink>();

        public IReadOnlyCollection<InvoiceLink> InvoiceLinks { get; set; } = Array.Empty<InvoiceLink>();

        public IReadOnlyCollection<PaymentForDocumentCreateResponse> AccountingStatements { get; set; } = Array.Empty<PaymentForDocumentCreateResponse>();

        public long? ContractBaseId { get; set; }

        /// <summary>
        /// Резерв (данная величина уменьшает сумму, покрываемую первичными документами)
        /// </summary>
        public decimal? ReserveSum { get; set; }

        /// <summary>
        /// Какое событие обрабатывается
        /// </summary>
        public HandleEventType EventType { get; set; }
        
        /// <summary>
        /// Обрабатывать ли связи со счетами (только в поступлениях)
        /// </summary>
        public bool CanHaveBills { get; set; }

        /// <summary>
        /// Существующие связи п/п (не только с документами)
        /// </summary>
        public IReadOnlyCollection<LinkWithDocument> ExistentLinks { get; set; }
    }
}
