using System.Collections.Generic;

namespace Moedelo.Docs.Dto.Docs
{
    public class ProductUnionDto
    {
        public long MasterProductId { get; set; }

        public List<long> SlaveProductIds { get; set; }
    }
}
