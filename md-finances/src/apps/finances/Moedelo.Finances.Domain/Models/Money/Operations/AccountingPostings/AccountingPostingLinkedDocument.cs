using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Operations.AccountingPostings
{
    public class AccountingPostingLinkedDocument
    {
        public string DocumentName { get; set; }

        public List<AccountingPosting> Postings { get; set; }
    }
}
