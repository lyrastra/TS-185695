using System.Collections.Generic;

namespace Moedelo.Finances.Domain.Models.Money.Operations.AccountingPostings
{
    public class AccountingPostingList
    {
        public bool IsManual { get; set; }
        
        public List<AccountingPosting> Postings { get; set; } = new List<AccountingPosting>();

        public List<AccountingPostingLinkedDocument> LinkedDocuments { get; set; } = new List<AccountingPostingLinkedDocument>();
    }
}