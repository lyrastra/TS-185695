using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Outsouce
{
    public class OutsourceProductDto
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Подтип
        /// </summary>
        public StockProductSubTypeEnum ProductSubType { get; set; }
    }
}