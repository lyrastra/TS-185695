using Moedelo.Common.Enums.Enums.Marketplaces;

namespace Moedelo.StockV2.Dto.Products
{
    public class ProductMarketplaceCodeDto
    {
        public long ProductId { get; set; }
        public MarketplaceType MarketplaceType { get; set; }
        public string MarketplaceCode { get; set; }
        public MarketplaceCodeType MarketplaceCodeType { get; set; }
        /// <summary>
        /// Вычисляемый признак: является ли код указанного типа основным для МП
        /// </summary>
        public bool IsPrimaryCodeType { get; set; }
    }
}
