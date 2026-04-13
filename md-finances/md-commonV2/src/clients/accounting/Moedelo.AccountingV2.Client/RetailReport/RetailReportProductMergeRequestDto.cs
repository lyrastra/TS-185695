using System.Collections.Generic;

namespace Moedelo.AccountingV2.Client.RetailReport
{
    public class RetailReportProductMergeRequestDto
    {
        public long DocumentBaseId { get; set; }
        public long PrimaryProductId { get; set; }
        public List<long> SecondaryProductIds { get; set; }
    }
}