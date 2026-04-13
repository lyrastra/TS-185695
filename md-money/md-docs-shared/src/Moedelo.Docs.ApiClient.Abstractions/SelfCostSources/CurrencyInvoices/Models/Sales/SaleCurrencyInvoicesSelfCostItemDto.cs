namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CurrencyInvoices.Models.Sales
{
    public class SaleCurrencyInvoicesSelfCostItemDto
    {
        /// <summary>
        /// Идентификатор позиции инвойса
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Идентификатор товара/материала.
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Кол-во товара/материала/услуг
        /// </summary>
        public decimal ItemCount { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal ItemSum { get; set; }
        
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
    }
}