namespace Moedelo.Common.Enums.Enums.AccountingPolicy
{
    /// <summary>
    /// Какую оценку применять при списании МПЗ методом "средней" или ФИФО.
    /// </summary>
    public enum ForWriteOffMpzByAverageCostOrFifo
    {
        /// <summary>
        /// Применять взвешенную оценку.
        /// </summary>
        UseBalancedAssessment = 1,

        /// <summary>
        /// Применять скользящую оценку.
        /// </summary>
        UseSlidingAssessment = 2
    }
}