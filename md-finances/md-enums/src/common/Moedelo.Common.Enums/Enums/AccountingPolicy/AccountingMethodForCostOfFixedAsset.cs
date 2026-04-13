namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Учет стоимости ОС, используемых в ОСНО и ЕНВД, пропорционально сумме выручки или иным способом.
    /// </summary>
    public enum AccountingMethodForCostOfFixedAsset
    {
        /// <summary>
        /// Пропорционально сумме выручки.
        /// </summary>
        ByAmountOfRevenue = 1,

        /// <summary>
        /// Иным способом.
        /// </summary>
        AnotherMethod = 2
    }
}