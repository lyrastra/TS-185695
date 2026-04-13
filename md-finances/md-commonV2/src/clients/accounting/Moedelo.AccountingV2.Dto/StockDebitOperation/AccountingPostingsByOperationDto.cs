using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.StockDebitOperation
{
    public class AccountingPostingsByOperationDto
    {
        public string OperationName { get; set; }

        public List<PostingDescriptionDto> Postings { get; set; }

        public bool IsManualEdit { get; set; }

        public List<AccountionPostingsByLinkedDocumentDto> LinkedDocuments { get; set; }
    }
}
