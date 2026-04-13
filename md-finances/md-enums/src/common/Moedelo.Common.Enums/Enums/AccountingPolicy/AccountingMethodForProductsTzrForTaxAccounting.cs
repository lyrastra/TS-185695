namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Способ приёма к учёту ТЗР товаров (траспортно заготовительные расходы)
    /// для целей налогового учёта.
    /// </summary>
    public enum AccountingMethodForProductsTzrForTaxAccounting
    {
        /// <summary>
        /// Учитываются в составе прямых расходов на продажу.
        /// </summary>
        InDirectSellingCosts = 1,

        /// <summary>
        /// Учитываются в стоимости товаров.
        /// </summary>
        InCostOfProducts = 2
    }
}