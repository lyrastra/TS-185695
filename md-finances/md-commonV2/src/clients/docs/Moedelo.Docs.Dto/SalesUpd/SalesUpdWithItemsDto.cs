using System.Collections.Generic;

namespace Moedelo.Docs.Dto.SalesUpd
{
    public class SalesUpdWithItemsDto
    {
        public SalesUpdDto Upd { get; set; }
        
        public List<SalesUpdItemDto> Items { get; set; }
    }
}