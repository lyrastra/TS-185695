
using System.Collections.Generic;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto
{
    public class MarketplaceCodesSaveRequestDto
    {
        public long ProductId { get; set; }
        public IReadOnlyCollection<MarketplaceCodeDto> MarketplaceCodes { get; set; }
    }
}
