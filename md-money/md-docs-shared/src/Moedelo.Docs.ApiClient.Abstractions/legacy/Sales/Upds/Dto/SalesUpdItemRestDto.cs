using System.Collections.Generic;
using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto
{
    public class SalesUpdItemRestDto
    {
        /// <summary>
        /// Тип позиции документа: 1 - услуга, 2 - товар
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// Наименование услуги/товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Количество (объем)
        /// </summary>
        public decimal Count { get; set; }

        /// <summary> 
        /// Цена без НДС 
        /// </summary>
        public decimal Price { get; set; }

        /// <summary> 
        /// Ставка НДС 
        /// </summary>
        public NdsType NdsType { get; set; }

        /// <summary>
        /// Товар (указывается, если ItemType = 2)
        /// </summary>
        public long? StockProductId { get; set; }

        /// <summary> 
        /// Сумма без НДС 
        /// </summary>
        public decimal SumWithoutNds { get; set; }

        /// <summary> 
        /// Сумма НДС 
        /// </summary>
        public decimal NdsSum { get; set; }

        /// <summary> 
        /// Сумма с НДС 
        /// </summary>
        public decimal SumWithNds { get; set; }

        /// <summary>
        /// Страна для импортируемого товара
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// ГТД для импортируемого товара
        /// </summary>
        public string Declaration { get; set; }

        /// <summary>
        /// Маркировочные коды
        /// </summary>
        public List<SalesUpdProductLabelRestDto> Labels { get; set; } = new List<SalesUpdProductLabelRestDto>();
    }
}