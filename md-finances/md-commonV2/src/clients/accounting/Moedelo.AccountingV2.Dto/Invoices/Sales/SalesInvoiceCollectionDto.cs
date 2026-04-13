using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Invoices.Sales
{
    public class SalesInvoiceCollectionDto
    {
        public int Count { get; set; }

        public List<SalesInvoiceCollectionItemDto> ResourceList { get; set; }

        public int TotalCount { get; set; }
    }
}