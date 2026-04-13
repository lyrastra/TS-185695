using System.Collections.Generic;

namespace Moedelo.AccountingV2.Client
{
    public class ProductMergeRequestDto
    {
        public long MergeId { get; set; }
        public long DocumentBaseId { get; set; }
        public long PrimaryProductId { get; set; }
        public List<long> SecondaryProductIds { get; set; }
    }
}