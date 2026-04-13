using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Bills;
using Moedelo.Common.Enums.Enums.Documents;
using Moedelo.Docs.Dto.ProductsTrace;

namespace Moedelo.Docs.Dto.PurchaseUpd.Rest
{
    public class PurchaseUpdItemDto
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
        /// </summary>
        public NdsType NdsType { get; set; }
        
        /// <summary>
        /// Страна производства (в случае импортируемого товара)
        /// </summary>
        public string Country { get; set; }
        
        /// <summary>
        /// ГТД (в случае импортируемого товара)
        /// </summary>
        public string Declaration { get; set; }
        
        /// <summary>
        /// Тип позиции в УПД (товар - 1 или услуга - 2)
        /// </summary>
        public ItemType Type { get; set; }

        /// <summary>
        /// Позиция с прочерками в ед. изм., кол-ве и цене (только услуги)
        /// </summary>
        public bool IsUnmeasurable { get; set; }

        /// <summary>
        /// Прослеживаемость товаров
        /// </summary>
        public IReadOnlyCollection<ProductTraceDto> ProductTrace { get; set; } = new List<ProductTraceDto>();

        /// <summary>
        /// Идентификатор НДС кода раздела 7 (Код операции)
        /// </summary>
        public int? NdsDeclarationSection7CodeId { get; set; }
    }
}