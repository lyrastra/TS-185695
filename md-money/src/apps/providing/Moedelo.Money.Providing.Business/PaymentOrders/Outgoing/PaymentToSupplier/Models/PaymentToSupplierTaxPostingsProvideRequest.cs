using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.PaymentOrders.PaymentToSupplier.Models
{
    internal class PaymentToSupplierTaxPostingsProvideRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public Kontragent Kontragent { get; set; }

        public bool IncludeNds { get; set; }

        public decimal? NdsSum { get; set; }

        public IReadOnlyCollection<BaseDocument> BaseDocuments { get; set; }

        public IReadOnlyCollection<PurchasesWaybill> Waybills { get; set; }

        public IReadOnlyCollection<PurchasesStatement> Statements { get; set; }

        public IReadOnlyCollection<PurchasesUpd> Upds { get; set; }

        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; } = Array.Empty<DocumentLink>();

        public bool IsPaid { get; set; }

        public bool IsManualTaxPostings { get; set; }

        public bool IsBadOperationState { get; set; }
    }
}
