namespace Moedelo.Common.Enums.Enums.Stocks
{
    /// <summary> Типы складов (Оптовый, розничный) </summary>
    public enum StockType
    {
        /// <summary>
        ///  Оптовый склад
        /// </summary>
        Wholesale = 1,

        /// <summary>
        /// Розничный склад
        /// </summary>
        Retail = 2,

        /// <summary>
        /// Cклад комиссионера
        /// </summary>
        CommissionAgent = 3,
    }
}