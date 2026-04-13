using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto
{
    public class SalesUpdWithItemsDto
    {
        public SalesUpdDto Upd { get; set; }
        
        public List<SalesUpdItemDto> Items { get; set; }
    }
}