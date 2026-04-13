using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Bills;
using Moedelo.Common.Enums.Enums.Nds;

namespace Moedelo.AccountingV2.Dto.Statements.Purchases
{
    public class PurchasesStatementItemSaveRequestDto
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
        /// Идентификатор кода операции (код НДС раздела 7)
        /// </summary>
        public int? NdsDeclarationSection7CodeId { get; set; }

        /// <summary>
        /// Позиция с прочерками в ед. изм., кол-ве и цене (только услуги)
        /// </summary>
        public bool IsUnmeasurable { get; set; }
    }
}