using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Outsouce
{
    public class OutsourceProductSynonymDto
    {
        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string UnitOfMeasurement { get; set; }

        /// <summary>
        /// Тип
        /// </summary>
        public StockProductTypeEnum Type { get; set; }

        /// <summary>
        /// Значение, отражающее степень соответствия названия продукта условиям поиска
        /// </summary>
        public int Rank { get; set; }
    }
}
