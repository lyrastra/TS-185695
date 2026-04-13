using System.Collections.Generic;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds.Dto
{
    public class PurchasesUpdWithItemsDto
    {
        public PurchasesUpdDto Upd { get; set; }

        public List<PurchasesUpdItemDto> Items { get; set; }
    }
}