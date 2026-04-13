using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.StockDebitOperation
{
    public class AccountionPostingsByLinkedDocumentDto
    {
        public string DocumentName { get; set; }

        public List<PostingDescriptionDto> Postings { get; set; }
    }
}
