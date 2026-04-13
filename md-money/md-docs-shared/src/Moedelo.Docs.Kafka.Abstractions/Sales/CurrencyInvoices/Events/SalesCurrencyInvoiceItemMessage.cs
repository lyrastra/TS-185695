namespace Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices.Events
{
    public class SalesCurrencyInvoiceItemMessage
    {
        /// <summary>
        /// Наименование товара/услуги
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Кол-во
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long? StockProductId { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Скидка
        /// </summary>
        public decimal DiscountRate { get; set; }
    }
}