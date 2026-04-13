using Moedelo.Docs.Enums;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Statements.Dto
{
    public class SalesStatementItemSaveRequestDto
    {
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
        /// Размер скидки в процентах
        /// </summary>
        public decimal? DiscountRate { get; set; }
    }
}