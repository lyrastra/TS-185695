using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.StockV2.Dto.Products
{
    public class SearchByNameArticleUnitAndTypeDto
    {
        public string Name { get; set; }

        public string Article { get; set; }

        public string UnitOfMeasurement { get; set; }
        
        public StockProductTypeEnum Type { get; set; }
    }
}
