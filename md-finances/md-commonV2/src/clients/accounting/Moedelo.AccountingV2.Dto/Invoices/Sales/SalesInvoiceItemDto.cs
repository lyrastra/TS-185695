using System.Collections.Generic;
using Moedelo.AccountingV2.Dto.Invoices.Common;
using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Bills;
using Moedelo.Common.Enums.Enums.Nds;

namespace Moedelo.AccountingV2.Dto.Invoices.Sales
{
    public class SalesInvoiceItemDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

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
        /// Тип позиции 1 - Товар 2 - Услуга
        /// </summary>
        public ItemType Type { get; set; }

        public CostSyntheticAccountCode? ActivityAccountCode { get; set; }

        /// <summary>
        /// Цена за одну позицию
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Способ расчёта НДС 
        /// -1 - Без НДС, 
        /// 0 - НДС 0%, 
        /// 5 - НДС 5%, 
        /// 7 - НДС 7%, 
        /// 10 - НДС 10%, 
        /// 18 - НДС 18%, 
        /// 20 - НДС 20%, 
        /// 22 - НДС 22%
        /// </summary>
        public NdsTypes NdsType { get; set; }

        /// <summary>
        /// Сумма без НДС; если указана, то используется в расчёте Суммы НДС и Суммы с НДС, если они пустые
        /// </summary>
        public decimal? SumWithoutNds { get; set; }

        /// <summary>
        /// Сумма НДС; если указана, то используется в расчёте Суммы без НДС и Суммы с НДС, если они пустые
        /// </summary>
        public decimal? NdsSum { get; set; }

        /// <summary>
        /// Cумма с НДС; если указана, то используется в расчёте Суммы НДС и Суммы без НДС, если они пустые
        /// </summary>
        public decimal? SumWithNds { get; set; }

        /// <summary>
        /// Товар/материал
        /// </summary>
        public long? StockProductId { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Исо код
        /// </summary>
        public string CountryIso { get; set; }

        /// <summary>
        /// Регистрационный номер таможенной декларации(если товар мпортируемый)
        /// </summary>
        public string Declaration { get; set; }

        /// <summary>
        /// Прослеживание товаров
        /// </summary>
        public List<ProductTraceDto> ProductTrace { get; set; } = new List<ProductTraceDto>();

        /// <summary>
        /// Позиция с прочерками в ед. изм., кол-ве и цене (только услуги)
        /// </summary>
        public bool IsUnmeasurable { get; set; }
    }
}
