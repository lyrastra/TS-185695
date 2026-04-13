using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Statements.Sales
{
    public class SalesStatementCollectionDto
    {
        public int Count { get; set; }

        public List<SalesStatementCollectionItemDto> ResourceList { get; set; }

        public int TotalCount { get; set; }
    }
}