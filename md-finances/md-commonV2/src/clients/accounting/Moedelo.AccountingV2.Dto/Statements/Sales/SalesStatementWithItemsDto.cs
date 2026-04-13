using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Statements.Sales
{
    public class SalesStatementWithItemsDto
    {
        public SalesStatementDocDto Document { get; set; }
        
        public List<SalesStatementItemDto> Items { get; set; }
    }
}