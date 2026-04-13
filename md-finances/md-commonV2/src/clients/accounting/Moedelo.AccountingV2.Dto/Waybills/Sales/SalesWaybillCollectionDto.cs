using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Waybills.Sales
{
    public class SalesWaybillCollectionDto
    {
        public int Count { get; set; }

        public List<SalesWaybillCollectionItemDto> ResourceList { get; set; }

        public int TotalCount { get; set; }
    }
}