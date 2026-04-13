using System.Collections.Generic;

namespace Moedelo.Docs.Dto.Upd
{
    public class UpdWithItemsDto
    {
        public UpdDto Upd { get; set; }
        
        public List<UpdItemDto> Items { get; set; }
    }
}