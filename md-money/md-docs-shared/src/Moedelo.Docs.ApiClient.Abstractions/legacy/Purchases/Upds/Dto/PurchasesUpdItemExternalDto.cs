using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds.Dto
{
    public class PurchasesUpdItemExternalDto
    {
        /// <summary>
        /// ID товара или материала (Для услуги поле не заполнено)
        /// </summary>
        public long? StockProductId { get; set; }

        /// <summary>
        /// Наименование позиции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Цена за одну позицию
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// Сумма без НДС
        /// </summary>
        public decimal SumWithoutNds { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal SumWithNds { get; set; }

        /// <summary>
        /// Способ расчёта НДС 
        /// 1 - Без НДС, 
        /// 2 - НДС 0%, 
        /// 3 - НДС 10%, 
        /// 4 - НДС 18%, 
        /// 5 - НДС 20%, 
        /// 6 - НДС 5%, 
        /// 7 - НДС 7%, 
        /// 8 - НДС 22% 
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Страна производства
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// ГТД
        /// </summary>
        public string Declaration { get; set; }

        /// <summary>
        /// Тип позиции в УПД (товар - 1 или услуга - 2)
        /// </summary>
        public ItemType Type { get; set; }
    }
}