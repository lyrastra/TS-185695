using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccountingV2.Dto.Api.ClientData
{
    public class ExpenditureDataClientData
    {
        public int? KontragentId { get; set; }

        public string KontragentName { get; set; }

        public bool UseNds { get; set; }

        public bool StockCheck { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public IList<ProductAndMaterialSubItemClientData> Items { get; set; }

        public AoInvoiceClientData Invoice { get; set; }
    }
}