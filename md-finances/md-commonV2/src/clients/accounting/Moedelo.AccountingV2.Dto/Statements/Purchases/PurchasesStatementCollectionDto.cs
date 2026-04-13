using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Statements.Purchases
{
    public class PurchasesStatementCollectionDto
    {
        public int Count { get; set; }

        public List<PurchasesStatementCollectionItemDto> ResourceList { get; set; }

        public int TotalCount { get; set; }
    }
}