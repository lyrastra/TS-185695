using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Statements.Dto
{
    public class PurchasesStatementWithItemsDto
    {
        public PurchasesStatementDocDto Document { get; set; }
        
        public List<PurchasesStatementItemResponseDto> Items { get; set; }
    }
}