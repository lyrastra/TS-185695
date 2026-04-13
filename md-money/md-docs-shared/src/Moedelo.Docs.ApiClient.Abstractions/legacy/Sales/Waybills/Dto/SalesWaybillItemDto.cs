using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Waybills.Dto
{
    /// <summary>
    /// Информация о позиции в накладной на продажу
    /// </summary>
    public class SalesWaybillItemDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID товара или материала (Для услуги поле не передавать)
        /// </summary>
        public long? StockProductId { get; set; }

        /// <summary>
        /// Наименование позиции (обязательное поле)
        /// [MaxLength(1000)]
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество (обязательное поле)
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// Единица измерения (обязательное поле)
        /// [MaxLength(100)]
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Тип позиции (обязательное поле)
        /// Допустимые значения: ItemType.Goods, ItemType.Service
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// Цена за одну позицию (обязательное поле)
        /// Максимальная точность 4 знака после запятой
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Способ расчёта НДС
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Сумма без НДС (если указана)
        /// </summary>
        public decimal? SumWithoutNds { get; set; }

        /// <summary>
        /// Сумма НДС (если указана)
        /// </summary>
        public decimal? NdsSum { get; set; }

        /// <summary>
        /// Cумма с НДС (если указана)
        /// </summary>
        public decimal? SumWithNds { get; set; }

        /// <summary>
        /// Размер скидки в процентах (если есть)
        /// Размер скидки должен находиться в пределах от 0 до 99,9%
        /// </summary>
        public decimal? DiscountRate { get; set; }
    }
}
