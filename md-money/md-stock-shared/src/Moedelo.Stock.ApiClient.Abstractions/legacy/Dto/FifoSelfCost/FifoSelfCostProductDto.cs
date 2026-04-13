using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost
{
    public class FifoSelfCostProductDto
    {
        /// <summary>
        /// Идентификатор складского товара
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Подтип товара 
        /// </summary>
        public StockProductSubTypeEnum ProductSubType { get; set; }
    }
}