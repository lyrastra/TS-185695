using Moedelo.Stock.Enums;
using System.Collections.Generic;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.StockOperations
{
    public class MarketplaceRequestDto
    {
        public MarketplaceType? MarketplaceType { get; set; }
        public IReadOnlyCollection<string> MpIds  { get; set; }
        public IReadOnlyCollection<string> Articles { get; set; }
        public IReadOnlyCollection<string> Names { get; set; }
    }
}