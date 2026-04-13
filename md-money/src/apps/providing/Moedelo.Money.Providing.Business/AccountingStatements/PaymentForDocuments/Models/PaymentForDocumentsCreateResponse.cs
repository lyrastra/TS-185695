using Moedelo.Money.Providing.Business.AccountingStatements.Models;
using System;

namespace Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models
{
    class PaymentForDocumentCreateResponse
    {
        public long PrimaryDocBaseId { get; set; }

        public DateTime PrimaryDocDate { get; set; }

        public AccountingStatement AccountingStatement { get; set; }
    }
}
