namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Распределение прямых расходов, прямо не относящихся к конкретному виду деятельности.
    /// </summary>
    public enum AllocationMethodForCostsAreNotRelatedToSpecificActivity
    {
        /// <summary>
        /// Пропорционально выручке от продажи.
        /// </summary>
        ByRevenuesFromSales = 1,

        /// <summary>
        /// Пропорционально заработной плате работников.
        /// </summary>
        ByDirectSalaryOfProductionWorkers = 2,

        /// <summary>
        /// Пропорционально стоимости материалов, непосредственно использованных в конкретном процессе.
        /// </summary>
        ByMaterialCosts = 3
    }
}