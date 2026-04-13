namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.ProductIncome.Models
{
    public class ProductIncomeItemDto
    {
        /// <summary>
        /// Числовой идентификатор товара
        /// </summary>
        public long StockProductId { get; set; }

        /// <summary>
        /// Количество
        /// </summary>
        public double Count { get; set; }

        /// <summary>
        /// Цена
        /// </summary>
        public double Price { get; set; }
    }
}
