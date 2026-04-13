namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.PriceLists
{
    public class PriceListItemDto
    {
        /// <summary>
        /// Идентификатор прайс-листа
        /// </summary>
        public int PriceListId { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Скидка или наценка в зависимости от знака
        /// </summary>
        public int Markup { get; set; }

        /// <summary>
        /// Цена за единицу товара без учета скидок, наценок, НДС
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Цена за единицу товара расчитанная как цена Price с учетом скидки и с учетом НДС
        /// </summary>
        public decimal TotalPrice { get; set; }
    }
}
