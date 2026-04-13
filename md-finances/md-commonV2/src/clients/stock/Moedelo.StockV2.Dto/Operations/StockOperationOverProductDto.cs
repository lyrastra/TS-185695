using Moedelo.Common.Enums.Enums.Stocks;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Dto.Operations
{
    /// <summary>
    /// Операция над товаром.
    /// Одна складская операция может производиться над несколькими позициями товаров/материалов.
    /// </summary>
    public class StockOperationOverProductDto
    {
        public long Id { get; set; }

        /// <summary>
        /// Склад, на котором производится операция 
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// Количество продукта
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// Цена продукта (актуально только на чтение)
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Сумма операции
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Флаг "За баланс" для требования-накладной
        /// </summary>
        public bool? IsOffbalance { get; set; }

        /// <summary>
        /// Код типа операции над продуктом
        /// </summary>
        public StockOperationTypeEnum TypeCode { get; set; }

        /// <summary>
        /// Код группы, в которую входит тип операции над продуктом
        /// </summary>
        public StockOperationParentTypeEnum TypeParentCode { get; set; }

        /// <summary>
        /// Продукт
        /// </summary>
        public StockProductDto Product { get; set; }
    }
}