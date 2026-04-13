namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Способ амортизации.
    /// </summary>
    public enum AmortizationMethodForFixedAssets
    {
        /// <summary>
        /// Линейный
        /// </summary>
        Linear = 1,

        /// <summary>
        /// Уменьшение остатка без использования коэффициента.
        /// </summary>
        DecreaseInBalanceWithoutUsingCoefficient = 2,

        /// <summary>
        /// Уменьшение остатка с использованием коэффициента.
        /// </summary>
        DecreaseInBalanceWithUsingCoefficient = 3,

        /// <summary>
        /// Списание стоимости по сумме чисел лет срока полезного использования.
        /// </summary>
        WriteDownBySumOfYearsOfUsefulLife = 4,

        /// <summary>
        /// Списание стоимости пропорционально объему продукции (работ).
        /// </summary>
        WriteDownByUnitsOfProductionOrWork = 5,

        /// <summary>
        /// Разные способы для различных групп.
        /// </summary>
        DifferentMethodsForDifferentGroups = 6,

        /// <summary>
        /// Нелинейный метод для налогового учета (за исключением зданий, сооружений, 
        /// передаточных устройств, входящих в восьмую-девятую амортизационные группы).
        /// </summary>
        NonlinearMethodForTaxAccounting = 7
    }
}