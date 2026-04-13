using System.Collections.Generic;

namespace Moedelo.Finances.Public.ClientData.Money.Operations.AccountingPostings
{
    public class AccountingPostingsClientData
    {
        public bool IsManualEdit { get; set; }
        
        public List<AccountingPostingDescriptionClientData> Postings { get; set; }

        public List<AccountingPostingLinkedDocumentClientData> LinkedDocuments { get; set; } = new List<AccountingPostingLinkedDocumentClientData>();
    }
}