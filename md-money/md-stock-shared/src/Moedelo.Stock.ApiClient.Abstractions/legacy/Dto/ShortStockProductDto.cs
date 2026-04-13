using Moedelo.Stock.Enums;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto
{
    /// <summary>
    /// Упрощённый объект с информацией о номенклатуре, нужен для списка номенклатур в финансисте
    /// </summary>
    public class ShortStockProductDto
    {
        /// <summary>
        /// ID номенклатуры
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Артикул
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Идентификатор cубконто
        /// </summary>
        public long? SubcontoId { get; set; }

        /// <summary>
        /// Тип номенклатуры
        /// </summary>
        public StockProductTypeEnum Type { get; set; }
    }
}
