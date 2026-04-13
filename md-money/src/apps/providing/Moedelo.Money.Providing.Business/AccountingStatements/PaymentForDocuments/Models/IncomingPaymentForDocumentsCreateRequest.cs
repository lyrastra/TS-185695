using System;
using System.Collections.Generic;
using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;

namespace Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models
{
    internal class IncomingPaymentForDocumentsCreateRequest
    {
        public long PaymentBaseId { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool IsMainKontragent { get; set; }

        public IReadOnlyCollection<DocumentLink> Links { get; set; }

        public IReadOnlyCollection<SalesWaybill> Waybills { get; set; }

        public IReadOnlyCollection<SalesStatement> Statements { get; set; }

        public IReadOnlyCollection<SalesUpd> Upds { get; set; }

        public Kontragent Kontragent { get; set; }

        public Contract Contract { get; set; }

        public bool IsBadOperationState { get; set; }

        public IReadOnlyCollection<LinkWithDocument> ExistentLinks { get; set; }
    }
}
