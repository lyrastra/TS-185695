namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Распределение прямых расходов, прямо не относящихся к конкретному виду деятельности.
    /// </summary>
    public enum AllocationMethodForIndirectCosts
    {
        /// <summary>
        /// Пропорционально прямой заработной плате основных производственных рабочих.
        /// </summary>
        ByDirectSalaryOfProductionWorkers = 1,

        /// <summary>
        /// Пропорционально прямым материальным затратам.
        /// </summary>
        ByDirectMaterialCosts = 2,

        /// <summary>
        /// Пропорционально выручке от реализации продукции (работ, услуг).
        /// </summary>
        ByRevenuesFromSales = 3
    }
}