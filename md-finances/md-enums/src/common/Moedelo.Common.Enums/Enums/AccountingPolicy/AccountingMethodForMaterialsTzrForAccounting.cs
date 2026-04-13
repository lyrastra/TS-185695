namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Способ приёма к учёту ТЗР материалов (траспортно заготовительные расходы)
    /// для целей бух. учёта.
    /// </summary>
    public enum AccountingMethodForMaterialsTzrForAccounting
    {
        /// <summary>
        /// Прямое включение ТЗР в фактическую себестоимость материала.
        /// </summary>
        ByInclusionInActualCostOfMaterial = 1,

        /// <summary>
        /// Отнесение ТЗР на отдельный субсчет к счету 10 «Материалы».
        /// </summary>
        ByAttributionOnSubAccountToAccountNumber10 = 2
    }
}