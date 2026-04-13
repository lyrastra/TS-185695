using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Statements.Purchases
{
    public class PurchasesStatementWithItemsDto
    {
        public PurchasesStatementDocDto Document { get; set; }
        
        public List<PurchasesStatementItemDto> Items { get; set; }
    }
}