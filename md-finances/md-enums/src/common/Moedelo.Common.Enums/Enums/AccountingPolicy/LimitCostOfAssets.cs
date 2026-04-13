namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Порядок ведения книги покупок, книги продаж, дополнительных листов к ним, журнала учета
    /// полученных и выданных счетов-фактур.
    /// </summary>
    public enum LimitCostOfAssets
    {
        /// <summary>
        /// 40000 руб. (значение устанавливается для бух.учета и налогового учета).
        /// </summary>
        FixLimit40000Rub = 1,

        /// <summary>
        /// Меньшая сумма.
        /// </summary>
        Less = 2,

        /// <summary>
        /// Без использования лимита.
        /// </summary>
        WithoutLimit = 3
    }
}