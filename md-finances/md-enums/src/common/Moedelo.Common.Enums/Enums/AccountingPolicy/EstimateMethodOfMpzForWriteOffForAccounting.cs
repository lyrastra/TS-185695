namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Способ оценки стоимости при списании для целей бухгалтерского учёта.
    /// </summary>
    public enum EstimateMethodOfMpzForWriteOffForAccounting
    {
        /// <summary>
        /// По средней себестоимости.
        /// </summary>
        ByAverageCost = 1,

        /// <summary>
        /// ФИФО.
        /// </summary>
        Fifo = 2,

        /// <summary>
        /// По стоимости единицы.
        /// </summary>
        ByUnitCost = 3,

        /// <summary>
        /// Разные способы для различных групп.
        /// </summary>
        DifferentMethodsForDifferentGroups = 4,
    }
}