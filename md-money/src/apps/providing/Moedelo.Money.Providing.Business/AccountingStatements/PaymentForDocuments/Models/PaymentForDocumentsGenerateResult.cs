using Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models;
using Moedelo.Money.Providing.Business.AccountingStatements.Models;
using System;

namespace Moedelo.Money.Providing.Business.AccountingStatements.PaymentForDocuments.Models
{
    class PaymentForDocumentsGenerateResult
    {
        public Guid TemporaryId { get; set; } = Guid.NewGuid();

        public AccountingStatement AccountingStatement { get; set; }

        public AccountingPosting AccoutingPosting { get; set; }

        public long PrimaryDocBaseId { get; set; }

        public DateTime PrimaryDocDate { get; set; }
    }
}
