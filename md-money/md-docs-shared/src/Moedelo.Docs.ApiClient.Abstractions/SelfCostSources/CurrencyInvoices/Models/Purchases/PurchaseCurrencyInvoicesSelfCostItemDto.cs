namespace Moedelo.Docs.ApiClient.Abstractions.SelfCostSources.CurrencyInvoices.Models.Purchases
{
    public class PurchaseCurrencyInvoicesSelfCostItemDto
    {
        /// <summary>
        /// Идентификатор позиции инвойса
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Сумма НДС
        /// </summary>
        public decimal? NdsSum { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Кол-во товара/материала/услуг
        /// </summary>
        public decimal ItemCount { get; set; }

        /// <summary>
        /// Идентификатор товара/материала.
        /// </summary>
        public long StockProductId { get; set; }
    }
}