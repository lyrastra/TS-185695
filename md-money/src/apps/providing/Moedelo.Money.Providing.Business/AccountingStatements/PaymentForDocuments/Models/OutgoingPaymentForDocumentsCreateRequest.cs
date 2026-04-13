using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using System;
using System.Collections.Generic;
using Moedelo.Money.Providing.Business.LinkedDocuments.Models;

namespace Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models
{
    class OutgoingPaymentForDocumentsCreateRequest
    {
        public long PaymentBaseId { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool IsMainKontragent { get; set; }

        public IReadOnlyCollection<DocumentLink> Links { get; set; }

        public IReadOnlyCollection<PurchasesWaybill> Waybills { get; set; }

        public IReadOnlyCollection<PurchasesStatement> Statements { get; set; }

        public IReadOnlyCollection<PurchasesUpd> Upds { get; set; }

        public Kontragent Kontragent { get; set; }

        public Contract Contract { get; set; }

        public bool IsPaid { get; set; }

        public bool IsBadOperationState { get; set; }

        public IReadOnlyCollection<LinkWithDocument> ExistentLinks { get; set; }
    }
}
