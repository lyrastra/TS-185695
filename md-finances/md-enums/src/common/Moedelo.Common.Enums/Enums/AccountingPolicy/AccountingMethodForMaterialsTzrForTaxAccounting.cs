namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Способ приёма к учёту ТЗР материалов (траспортно заготовительные расходы)
    /// для целей налогового учёта.
    /// </summary>
    public enum AccountingMethodForMaterialsTzrForTaxAccounting
    {
        /// <summary>
        /// Прямое включение ТЗР в фактическую себестоимость материала.
        /// </summary>
        ByInclusionInActualCostOfMaterial = 1,

        /// <summary>
        /// Учитываются в составе расходов на производство и реализацию.
        /// </summary>
        RecognizedAsExpenseOnProductionAndSale = 2
    }
}