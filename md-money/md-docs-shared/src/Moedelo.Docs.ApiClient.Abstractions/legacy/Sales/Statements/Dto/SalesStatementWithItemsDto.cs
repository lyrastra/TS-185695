using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Statements.Dto
{
    public class SalesStatementWithItemsDto
    {
        public SalesStatementDocDto Document { get; set; }
        
        public List<SalesStatementItemDto> Items { get; set; }
    }
}