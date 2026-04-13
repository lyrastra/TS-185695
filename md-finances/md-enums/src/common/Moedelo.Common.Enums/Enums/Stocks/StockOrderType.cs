namespace Moedelo.Common.Enums.Enums.Stocks
{
    /// <summary>
    /// Возможные варианты заказов
    /// </summary>
    public enum StockOrderType
    {
        /// <summary>
        /// Заказ покупателя
        /// </summary>
        CustomerOrder = 0,

        /// <summary>
        /// Заказ поставщику
        /// </summary>
        SupplierOrder = 1,

        /// <summary>
        /// Сборка
        /// </summary>
        OrderPack = 2
    }
}
