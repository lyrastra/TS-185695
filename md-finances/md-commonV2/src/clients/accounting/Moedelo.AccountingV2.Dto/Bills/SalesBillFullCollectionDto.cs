using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Bills
{
    public class SalesBillFullCollectionDto
    {
        public int Count { get; set; }

        public List<SalesBillFullDto> ResourceList { get; set; }

        public int TotalCount { get; set; }
    }
}