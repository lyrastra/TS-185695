using System.Collections.Generic;

namespace Moedelo.StockV2.Dto.Operations.ProductMerge
{
    public class ProductMergeRequestDto
    {
        public long PrimaryProductId { get; set; }

        public IReadOnlyCollection<long> SecondaryProductIds { get; set; }
    }
}
