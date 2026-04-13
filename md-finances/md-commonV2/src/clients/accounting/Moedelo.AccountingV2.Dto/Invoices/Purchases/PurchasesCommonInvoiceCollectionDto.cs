using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Invoices.Purchases
{
    public class PurchasesCommonInvoiceCollectionDto
    {
        public int Count { get; set; }

        public List<PurchasesCommonInvoiceCollectionItemDto> ResourceList { get; set; }

        public int TotalCount { get; set; }
    }
}