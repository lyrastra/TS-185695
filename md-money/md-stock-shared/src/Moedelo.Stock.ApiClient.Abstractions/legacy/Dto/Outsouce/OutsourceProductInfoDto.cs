using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Outsouce
{
    public class OutsourceProductInfoDto
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
        /// Тип
        /// </summary>
        public StockProductTypeEnum Type { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string UnitOfMeasurement { get; set; }
    }
}