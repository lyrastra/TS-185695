using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Money.Operations.AccountingPostings
{
    public class AccountingPostingLinkedDocumentClientData
    {
        public string DocumentName { get; set; }

        public List<AccountingPostingDescriptionClientData> Postings { get; set; }
    }
}