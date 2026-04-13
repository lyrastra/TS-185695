using System.Collections.Generic;

namespace Moedelo.CommonV2.EventBus.Stocks
{
    public class ProductMergeEvent
    {
        public int FirmId { get; set; }
        
        public int UserId { get; set; }

        public long PrimaryProductId { get; set; }

        public List<long> SecondaryProductIds { get; set; }

        public int ProductMergeResultId { get; set; }
    }
}