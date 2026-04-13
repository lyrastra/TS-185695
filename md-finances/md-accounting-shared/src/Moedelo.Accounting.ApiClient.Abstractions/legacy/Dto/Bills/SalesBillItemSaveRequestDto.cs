using Moedelo.Accounting.Enums;
using Moedelo.Accounting.Enums.Documents;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Bills
{
    public class SalesBillItemSaveRequestDto
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
        /// MaxLength(1000)
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
        /// Тип позиции. (обязательное поле)
        /// Допустимые значения: Goods, Service
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// Цена за одну позицию (обязательное поле)
        /// Точность: 4 знака после запятой
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Способ расчёта НДС -1 - Без НДС, 0 - НДС 0%, 10 - НДС 10%, 18 - НДС 18%, 20 - НДС 20%
        /// Допустимые значения: NdsTypes.Nds0, NdsTypes.Nds10, NdsTypes.Nds18, NdsTypes.Nds20, NdsTypes.None
        /// </summary>
        public NdsTypes NdsType { get; set; }

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
        /// Размер скидки в процентах
        /// Ограничение: Размер скидки должен находиться в пределах от 0 до 99,9%
        /// </summary>
        public decimal? DiscountRate { get; set; }
    }
}