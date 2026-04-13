using Moedelo.Money.Providing.Business.Abstractions.Models;
using Moedelo.Money.Providing.Business.Contracts;
using Moedelo.Money.Providing.Business.Kontragents;
using Moedelo.Money.Providing.Business.PrimaryDocuments.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models
{
    class OutgoingPaymentForDocumentsGenerateRequest
    {
        public DateTime PaymentDate { get; set; }

        public bool IsMainKontragent { get; set; }

        public IReadOnlyCollection<DocumentLink> Links { get; set; }

        public IReadOnlyCollection<PurchasesWaybill> Waybills { get; set; }

        public IReadOnlyCollection<PurchasesStatement> Statements { get; set; }

        public IReadOnlyCollection<PurchasesUpd> Upds { get; set; }

        public Kontragent Kontragent { get; set; }

        public Contract Contract { get; set; }
    }
}
