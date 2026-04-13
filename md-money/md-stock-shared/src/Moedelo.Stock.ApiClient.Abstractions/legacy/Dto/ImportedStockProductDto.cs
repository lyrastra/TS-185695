
namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto
{
    public class ImportedStockProductDto
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор товара в маркетплейсе
        /// </summary>
        public string MarketplaceId { get; set; }
    }
}