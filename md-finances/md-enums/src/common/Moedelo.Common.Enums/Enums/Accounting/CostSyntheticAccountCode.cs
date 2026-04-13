namespace Moedelo.Common.Enums.Enums.Accounting
{
    /// <summary>
    /// Используется при формирования проводок для определения счета и субконто
    /// в зависимости от выбранного значения
    /// </summary>
    public enum CostSyntheticAccountCode
    {
        /// <summary>
        /// Cчет 20
        /// </summary>
        _20 = 200000,

        /// <summary>
        ///Счет 26
        /// </summary>
        _26 = 260000,

        /// <summary>
        ///Счет 44 с субконто "Работы и услуги сторонних лиц"
        /// </summary>
        _44_Services = 440000,

        /// <summary>
        ///Счет 44 субконто "Доставка товара"
        /// </summary>
        _44_GoodsDelivery = 440201,

        /// <summary>
        ///Счет 91
        /// </summary>
        _91 = 910000,

        /// <summary>
        ///Счет 97
        /// </summary>
        _97 = 970000,
    }
}
