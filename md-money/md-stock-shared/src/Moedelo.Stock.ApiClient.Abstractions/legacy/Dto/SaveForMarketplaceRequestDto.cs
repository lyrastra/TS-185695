using System.Collections.Generic;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto
{
    public class SaveForMarketplaceRequestDto
    {
        public long ProductId { get; set; }
        public string Article { get; set; }
        public IReadOnlyCollection<MarketplaceCodeDto> MarketplaceCodes { get; set; }
    }
}
