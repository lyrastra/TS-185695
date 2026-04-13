using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Waybills.Purchases
{
    public class PurchasesWaybillCollectionDto
    {
        public int Count { get; set; }

        public List<PurchasesWaybillCollectionItemDto> ResourceList { get; set; }

        public int TotalCount { get; set; }
    }
}