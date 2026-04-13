namespace Moedelo.AccountingV2.Dto.Ndfl3Validator
{
    public class StockProductBalanceDto
    {
        /// <summary>
        /// Идентификатор складской позиции
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Идентификатор скалада
        /// </summary>
        public long StockId { get; set; }

        /// <summary>
        /// Наименование товара
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Количество товара на складе
        /// </summary>
        public decimal Balance { get; set; }
    }
}