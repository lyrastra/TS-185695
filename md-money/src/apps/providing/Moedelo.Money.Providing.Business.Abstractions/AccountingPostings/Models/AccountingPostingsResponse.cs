using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.Abstractions.AccountingPostings.Models
{
    public class AccountingPostingsResponse
    {
        public IReadOnlyCollection<LinkedDocumentAccountingPostings> LinkedDocuments { get; set; } = Array.Empty<LinkedDocumentAccountingPostings>();

        public IReadOnlyCollection<AccountingPosting> Postings { get; set; } = Array.Empty<AccountingPosting>();
    }
}
