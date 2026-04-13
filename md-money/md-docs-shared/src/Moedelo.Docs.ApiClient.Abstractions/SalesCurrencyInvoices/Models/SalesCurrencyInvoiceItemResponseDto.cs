namespace Moedelo.Docs.ApiClient.Abstractions.SalesCurrencyInvoices.Models
{
    public class SalesCurrencyInvoiceItemResponseDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор инвойса
        /// </summary>
        public long CurrencyInvoiceId { get; set; }

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
        public decimal ItemCount { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal ItemSum { get; set; }

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