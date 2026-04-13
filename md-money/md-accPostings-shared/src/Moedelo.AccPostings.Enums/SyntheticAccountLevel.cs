namespace Moedelo.AccPostings.Enums
{
    /// <summary>
    /// Порядок бухгалтерского счета
    /// </summary>
    public enum SyntheticAccountLevel
    {
        /// <summary>
        /// Счет первого порядка (синтетический счет, например, 44)
        /// </summary>
        First = 1,
        
        /// <summary>
        /// Счет второго порядка (субсчет, например, 44.01)
        /// </summary>
        Second = 2,
        
        /// <summary>
        /// Счет третьего порядка (аналитический счет, например, 44.01.01)
        /// </summary>
        Third = 3
    }
}