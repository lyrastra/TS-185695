using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.StockV2.Dto.Products
{
    public class CheckProductExistenceDto
    {
        public string ProductName { get; set; }

        public string Article { get; set; }
        
        public StockProductTypeEnum Type { get; set; }
    }
}
