using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.StockDebitOperation
{
    public class AccountingPostingsByDocumentDto
    {
        public List<AccountingPostingsByOperationDto> Operations { get; set; }

        public string Message { get; set; }

        public bool Status { get; set; }
    }
}
