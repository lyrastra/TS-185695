using Moedelo.Common.Enums.Enums.Bills;
using Moedelo.Common.Enums.Enums.Documents;
using System.Collections.Generic;
using Moedelo.Docs.Dto.ProductsTrace;

namespace Moedelo.Docs.Dto.SalesUpd.Rest
{
    public class SalesUpdItemSaveRequestRestDto
    {
        /// <summary>
        /// Идентификатор товара/материала (для услуги не заполняется)
        /// </summary>
        public long? StockProductId { get; set; }

        /// <summary>
        /// Наименование позиции
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Цена за одну позицию
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Сумма без НДС
        /// </summary>
        public decimal? SumWithoutNds { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal? NdsSum { get; set; }

        /// <summary>
        /// Cумма с НДС
        /// </summary>
        public decimal? SumWithNds { get; set; }

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

        /// <summary>
        /// Маркировочные коды
        /// </summary>
        public List<ProductLabelRestDto> Labels { get; set; } = new List<ProductLabelRestDto>();
        
        /// <summary>
        /// Прослеживание товаров
        /// </summary>
        public IReadOnlyCollection<ProductTraceDto> ProductTrace { get; set; } = new List<ProductTraceDto>();

        /// <summary>
        /// Идентификатор НДС кода раздела 7 (Код операции)
        /// </summary>
        public int? NdsDeclarationSection7CodeId { get; set; }

        /// <summary>
        /// Позиция с прочерками в ед. изм., кол-ве и цене (только услуги)
        /// </summary>
        public bool IsUnmeasurable { get; set; }

        /// <summary>
        /// Указал ли пользователь суммы сам
        /// </summary>
        public bool IsCustomSums { get; set; }

        /// <summary>
        /// Код вида операции, если тип УПД = 1 (УПД является также сч-ф)
        /// </summary>
        public NdsOperationCodes? NdsOperationType { get; set; }
    }
}