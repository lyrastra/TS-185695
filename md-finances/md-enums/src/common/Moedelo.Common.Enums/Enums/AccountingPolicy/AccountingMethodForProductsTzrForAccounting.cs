namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Способ приёма к учёту ТЗР товаров (траспортно заготовительные расходы)
    /// для целей бух. учёта.
    /// </summary>
    public enum AccountingMethodForProductsTzrForAccounting
    {
        /// <summary>
        /// Учитывать отдельно на счете 44 «Расходы на продажу».
        /// </summary>
        ByAttributionToAccountNumber44 = 1,

        /// <summary>
        /// Включаются в себестоимость приобретенных товаров.
        /// </summary>
        ByIncludingInCostOfPurchasedProducts = 2
    }
}