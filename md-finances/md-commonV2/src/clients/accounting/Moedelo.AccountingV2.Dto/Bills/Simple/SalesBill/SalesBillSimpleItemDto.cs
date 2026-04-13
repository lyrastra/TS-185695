namespace Moedelo.AccountingV2.Dto.Bills.Simple.SalesBill
{
    /// <summary>
    /// Облегчённая модель позиции счёта
    /// </summary>
    public class SalesBillSimpleItemDto
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
        /// </summary>
        public string Name { get; set; }
    }
}