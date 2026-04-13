namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary> Тип расчёта дополнительного взноса </summary>
    public enum AdditionalPaymentCalculationType
    {
        /// <summary>
        /// Расчёт доп. взноса без учёта расходов
        /// </summary>
        WithoutExpenses = 1,

        /// <summary>
        /// Расчёт доп. взноса с учётом расходов
        /// </summary>
        WithExpenses = 2
    }
}