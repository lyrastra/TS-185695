namespace Moedelo.Docs.Kafka.Abstractions.Purchases.CurrencyInvoices.Messages
{
    public class PurchaseCurrencyInvoiceItemMessage
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
        /// НДС
        /// </summary>
        public decimal? NdsSum { get; set; }
        
        /// <summary>
        /// Таможенная пошлина
        /// </summary>
        public decimal? CustomsDutySum { get; set; }
    }
}