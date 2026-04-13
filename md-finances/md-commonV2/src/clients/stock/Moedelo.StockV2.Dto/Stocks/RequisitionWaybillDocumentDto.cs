using System;
using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.StockV2.Dto.Stocks
{
    public class RequisitionWaybillDocumentDto
    {
        public long Id { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DocumentDate { get; set; }

        public int? WorkerId { get; set; }

        public bool IsOtherOutgo { get; set; }

        public bool? IsNonOperatingOutgo { get; set; }

        public long? DocumentBaseId { get; set; }

        public decimal Sum { get; set; }

        public ProvidePostingType TaxPostingType { get; set; }
    }
}
