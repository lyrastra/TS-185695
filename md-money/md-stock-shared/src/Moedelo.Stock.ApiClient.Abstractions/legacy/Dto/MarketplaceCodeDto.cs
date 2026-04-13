using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto
{
    public class MarketplaceCodeDto
    {
        public MarketplaceType Type { get; set; }
        public string Code { get; set; }
        public MarketplaceCodeType CodeType { get; set; }
    }
}