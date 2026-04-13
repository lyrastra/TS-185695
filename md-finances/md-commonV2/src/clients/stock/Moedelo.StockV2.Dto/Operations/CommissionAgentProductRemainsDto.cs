namespace Moedelo.StockV2.Dto.Operations
{
    public class CommissionAgentProductRemainsDto
    {
        /// <summary>
        /// Товар, по которому считается остаток
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Склад, по которому считается остаток товара
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// Кол-во оставшегося товара
        /// </summary>
        public decimal RemainCount { get; set; }

        /// <summary>
        /// Сумма остатка товара, в предыдущем периоде. Эту сумму нужно сторнировать
        /// </summary>
        public decimal RemainStornoSum { get; set; }

        /// <summary>
        /// Сумма остатка товара, в закрываемом периоде.
        /// </summary>
        public decimal RemainSum { get; set; }
    }
}