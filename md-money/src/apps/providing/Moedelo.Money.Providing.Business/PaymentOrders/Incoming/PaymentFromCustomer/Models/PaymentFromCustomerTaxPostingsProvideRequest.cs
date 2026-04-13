using System;
using System.Collections.Generic;
using Moedelo.Money.Enums;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;

namespace Moedelo.Money.Providing.Business.PaymentOrders.Incoming.PaymentFromCustomer.Models
{
    internal class PaymentFromCustomerTaxPostingsProvideRequest
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public Kontragent Kontragent { get; set; }

        /// <summary>
        /// НДС
        /// </summary>
        public bool IncludeNds { get; set; }
        public decimal? NdsSum { get; set; }

        /// <summary>
        /// НДС для посредничества
        /// </summary>
        public decimal? MediationNdsSum { get; set; }

        /// <summary>
        /// Признак посредничества
        /// </summary>
        public bool IsMediation { get; set; }

        /// <summary>
        /// Вознаграждение посредника
        /// </summary>
        public decimal? MediationCommissionSum { get; set; }

        public IReadOnlyCollection<BaseDocument> BaseDocuments { get; set; }

        public IReadOnlyCollection<SalesWaybill> Waybills { get; set; }

        public IReadOnlyCollection<SalesStatement> Statements { get; set; }

        public IReadOnlyCollection<SalesUpd> Upds { get; set; }

        public IReadOnlyCollection<DocumentLink> DocumentLinks { get; set; } = Array.Empty<DocumentLink>();

        public TaxationSystemType TaxationSystemType { get; set; }

        public bool IsManualTaxPostings { get; set; }

        public bool IsBadOperationState { get; set; }
    }
}
