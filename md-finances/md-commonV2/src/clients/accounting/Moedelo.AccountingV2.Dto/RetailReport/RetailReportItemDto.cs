namespace Moedelo.AccountingV2.Dto.RetailReport
{
    public class RetailReportItemDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Id товара или материала
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
    }
}
