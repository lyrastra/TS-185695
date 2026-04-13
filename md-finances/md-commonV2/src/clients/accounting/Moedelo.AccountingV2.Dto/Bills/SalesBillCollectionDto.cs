using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Bills
{
    public class SalesBillCollectionDto
    {
        public int Count { get; set; }

        public List<SalesBillCollectionItemDto> ResourceList { get; set; }

        public int TotalCount { get; set; }
    }
}